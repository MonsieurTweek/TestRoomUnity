using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CardCanvasController : MonoBehaviour
{
    [Header("References")]
    public GameObject root = null;
    public ProgressBarController confirmBar = null;
    public List<CardController> cards = new List<CardController>();

    [Header("Properties")]
    public float confirmDelay = 0.25f;
    public List<Perk> perks = new List<Perk>();

    private HashSet<int> _perksToDisplay = new HashSet<int>();
    private List<Perk> _perksAvailable = new List<Perk>();
    private int _currentCardIndex = 0;
    private int _currentIntroCounter = 0;

    private int _tweenId = -1;

    private void Awake()
    {
        root.SetActive(false);
    }

    private void Start()
    {
        PerkGameEvent.instance.onDisplayed += OnPerkDisplayed;
        PerkGameEvent.instance.onUnlockStarted += OnPerkUnlockStarted;

        for (int i = 0; i < perks.Count; i++)
        {
            if (perks[i].isUnlock == true)
            {
                _perksAvailable.Add(perks[i]);
            }
        }
    }

    private void OnNavigatePerformed(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        int index = _currentCardIndex;

        if (input.x < 0f)
        {
            index--;
        }
        else if (input.x > 0f)
        {
            index++;
        }

        if (index >= 0 && index < cards.Count)
        {
            cards[_currentCardIndex].Deselect();

            _currentCardIndex = index;

            cards[_currentCardIndex].Select();

            AudioManager.instance.PlayMenuSound(AudioManager.instance.menuNavigationSfx);
        }
    }

    private void OnConfirmStarted(InputAction.CallbackContext context)
    {
        _tweenId = LeanTween.value(gameObject, 0f, 100f, confirmDelay).setOnUpdate(ConfirmProgress).setOnComplete(ConfirmComplete).id;

        // Unbind input except Confirm.Canceled so we are sure to don't switch during selection
        InputManager.instance.menu.Navigate.performed -= OnNavigatePerformed;
        InputManager.instance.menu.Confirm.started -= OnConfirmStarted;
    }

    private void ConfirmProgress(float progress)
    {
        confirmBar.current = Mathf.RoundToInt(progress);
    }

    private void ConfirmComplete()
    {
        AudioManager.instance.PlayMenuSound(AudioManager.instance.menuConfirmationSfx);

        cards[_currentCardIndex].ConfirmSelection();

        // Unbind Confirm.Canceled as the selection has been done
        InputManager.instance.menu.Confirm.canceled -= OnConfirmCanceled;
    }

    private void OnConfirmCanceled(InputAction.CallbackContext context)
    {
        if (LeanTween.isTweening(_tweenId) == true)
        {
            LeanTween.cancel(gameObject, _tweenId);

            confirmBar.current = 0;
        }

        // Rebind input as player can make another selection/confirmation
        // except Confirm.Canceled still bound
        InputManager.instance.menu.Navigate.performed += OnNavigatePerformed;
        InputManager.instance.menu.Confirm.started += OnConfirmStarted;
    }

    private void OnPerkDisplayed()
    {
        root.SetActive(true);

        confirmBar.current = 0;

        _currentIntroCounter = 0;
        _perksToDisplay.Clear();

        int index = 0;

        for (int i = 0; i < cards.Count; ++i)
        {
            do
            {
                index = Random.Range(0, _perksAvailable.Count);
            }
            while (_perksToDisplay.Contains(index) && _perksAvailable.Count > 1);

            _perksToDisplay.Add(index);

            cards[i].Initialize(_perksAvailable[index]);
            cards[i].AnimateIntro(OnIntroCompleted);
        }
    }

    private void OnIntroCompleted()
    {
        _currentIntroCounter++;

        // Check if all intro have been done
        if (_currentIntroCounter >= cards.Count)
        {
            cards[_currentCardIndex].Select();

            InputManager.instance.menu.Navigate.performed += OnNavigatePerformed;
            InputManager.instance.menu.Confirm.started += OnConfirmStarted;
            InputManager.instance.menu.Confirm.canceled += OnConfirmCanceled;
        }
    }

    private void OnPerkUnlockStarted(uint id, Perk perk)
    {
        for (int i = 0; i < cards.Count; ++i)
        {
            if (cards[i].data.uniqueId == id)
            {
                cards[i].AnimateUnlock(OnUnlockEnded);

                if (perk.isCumulative == false)
                {
                    _perksAvailable.Remove(perk);
                }
            }
            else
            {
                cards[i].AnimateHide();
            }
        }
    }

    private void OnUnlockEnded(object id)
    {
        for (int i = 0; i < cards.Count; ++i)
        {
            if (cards[i].data.uniqueId == (uint)id)
            {
                cards[i].Hide();

                PerkGameEvent.instance.EndUnlock(cards[i].data);
            }
        }

        root.SetActive(false);
    }

    private void OnDestroy()
    {
        InputManager.instance.menu.Navigate.performed -= OnNavigatePerformed;
        InputManager.instance.menu.Confirm.started -= OnConfirmStarted;
        InputManager.instance.menu.Confirm.canceled -= OnConfirmCanceled;

        if (PerkGameEvent.instance != null)
        {
            PerkGameEvent.instance.onDisplayed -= OnPerkDisplayed;
            PerkGameEvent.instance.onUnlockStarted -= OnPerkUnlockStarted;
        }
    }
}