using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections;

public class AnimatedPageController : MonoBehaviour
{
    public Selectable firstElement = null;

    private int _currentSelectedChild = 0;
    private List<Selectable> _selectableChilds = new List<Selectable>();

    private int _animationCounter = 0;
    private bool _isAnimationPlaying = false;

    private void Awake()
    {
        GetSelectableChilds();
    }

    private void GetSelectableChilds()
    {
        _selectableChilds.Clear();
        _selectableChilds.AddRange(GetComponentsInChildren<Selectable>());

        _currentSelectedChild = _selectableChilds.IndexOf(firstElement);

        if (_currentSelectedChild < _selectableChilds.Count)
        {
            _selectableChilds[_currentSelectedChild].OnSelect(null);
        }
    }

    private void OnEnable()
    {
        InputManager.instance.menu.Navigate.performed += OnNavigate;
        InputManager.instance.menu.Confirm.started += OnConfirmStarted;
        InputManager.instance.menu.Confirm.canceled += OnConfirmCanceled;
    }

    private void OnDisable()
    {
        InputManager.instance.menu.Navigate.performed -= OnNavigate;
        InputManager.instance.menu.Confirm.started -= OnConfirmStarted;
        InputManager.instance.menu.Confirm.canceled -= OnConfirmCanceled;
    }

    private void OnNavigate(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        Selectable tileToSelect = null;

        if (input.x < 0f && Mathf.Abs(input.x) > Mathf.Abs(input.y))
        {
            tileToSelect = _selectableChilds[_currentSelectedChild].navigation.selectOnLeft;
        }
        else if (input.x > 0f && Mathf.Abs(input.x) > Mathf.Abs(input.y))
        {
            tileToSelect = _selectableChilds[_currentSelectedChild].navigation.selectOnRight;
        }
        else if (input.y > 0f)
        {
            tileToSelect = _selectableChilds[_currentSelectedChild].navigation.selectOnUp;
        }
        else if (input.y < 0f)
        {
            tileToSelect = _selectableChilds[_currentSelectedChild].navigation.selectOnDown;
        }

        if (tileToSelect != null)
        {
            Select(_selectableChilds.IndexOf(tileToSelect));
        }
    }

    private void OnConfirmStarted(InputAction.CallbackContext context)
    {
        ((TileController)_selectableChilds[_currentSelectedChild]).ConfirmSelection();
    }

    private void OnConfirmCanceled(InputAction.CallbackContext context)
    {
        ((TileController)_selectableChilds[_currentSelectedChild]).CancelSelection();
    }

    private void Select(int index)
    {
        _selectableChilds[_currentSelectedChild].OnDeselect(null);

        _currentSelectedChild = index;

        _selectableChilds[_currentSelectedChild].OnSelect(null);
    }

    public void Show(float time, float delay)
    {
        _isAnimationPlaying = true;

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);

            child.localScale = Vector3.zero;

            LeanTween.scale(child.gameObject, Vector3.one, Random.Range(0f, time)).setDelay(Random.Range(0f, delay)).setOnComplete(OnShowProgress);
        }

        gameObject.SetActive(true);
    }

    private void OnShowProgress()
    {
        _animationCounter++;

        if (_animationCounter >= transform.childCount)
        {
            OnShowComplete();
        }
    }

    private void OnShowComplete()
    {
        _animationCounter = 0;
    }

    public void Hide(float time)
    {
        if (_isAnimationPlaying == false)
        {
            _isAnimationPlaying = true;

            for (int i = 0; i < transform.childCount; i++)
            {
                LeanTween.scale(transform.GetChild(i).gameObject, Vector3.zero, time).setOnComplete(OnHideProgress);
            }
        }
        else
        {
            OnHideComplete();
        }
    }

    private void OnHideProgress()
    {
        _animationCounter++;

        if (_animationCounter >= transform.childCount)
        {
            OnHideComplete();
        }
    }

    private void OnHideComplete()
    {
        gameObject.SetActive(false);
    }
}