using UnityEngine;

public class AnimatedPageController : MonoBehaviour
{
    private int _animationCounter = 0;
    private bool _isAnimationPlaying = false;

    public void Show(float time)
    {
        _isAnimationPlaying = true;

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).localScale = Vector3.zero;

            LeanTween.scale(transform.GetChild(i).gameObject, Vector3.one, time).setOnComplete(OnShowProgress);
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
                LeanTween.scale(transform.GetChild(i).gameObject, Vector3.zero, time).setOnComplete(OnHideComplete);
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
