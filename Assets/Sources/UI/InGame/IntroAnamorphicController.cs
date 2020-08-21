using UnityEngine;

public class IntroAnamorphicController : MonoBehaviour
{
    [Header("References")]
    public Transform layoutTop = null;
    public Transform layoutBottom = null;

    [Header("Properties")]
    public LeanTweenType animationType = LeanTweenType.linear;
    public float animationDuration = 0.5f;
    
    private void Start()
    {
        CharacterGameEvent.instance.onIntroStarted += OnIntroStarted;
        CharacterGameEvent.instance.onIntroEnded += OnIntroEnded;
    }

    private void OnIntroStarted(Transform target, AbstractCharacterData data)
    {
        // Size is reset to be invisible
        layoutTop.localScale = new Vector3(1f, 0f, 1f);
        layoutBottom.localScale = new Vector3(1f, 0f, 1f);

        LeanTween.scaleY(layoutTop.gameObject, 1f, animationDuration).setEase(animationType);
        LeanTween.scaleY(layoutBottom.gameObject, 1f, animationDuration).setEase(animationType);

        layoutTop.gameObject.SetActive(true);
        layoutBottom.gameObject.SetActive(true);
    }

    private void OnIntroEnded()
    {
        LeanTween.scaleY(layoutTop.gameObject, 0f, animationDuration * 0.5f).setEase(animationType);
        LeanTween.scaleY(layoutBottom.gameObject, 0f, animationDuration * 0.5f).setEase(animationType);
    }

    private void OnDestroy()
    {
        if (CharacterGameEvent.instance != null)
        {
            CharacterGameEvent.instance.onIntroStarted -= OnIntroStarted;
            CharacterGameEvent.instance.onIntroEnded -= OnIntroEnded;
        }
    }
}