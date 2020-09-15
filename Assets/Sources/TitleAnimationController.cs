using UnityEngine;
using System.Collections;

public class TitleAnimationController : MonoBehaviour
{
    public GameObject cameraCanvas = null;
    public GameObject overlayCanvas = null;

    public LeanTweenType introAnimation = LeanTweenType.linear;
    public AudioClip introSound = null;
    public AudioClip introMusic = null;
    public float introDuration = 3f;
    public float introDelay = 0.5f;

    private void Start()
    {
        StartCoroutine(StartAnimation());
    }

    private IEnumerator StartAnimation()
    {
        yield return new WaitForSeconds(introDelay);

        LeanTween.scale(cameraCanvas, Vector3.one, introDuration).setEase(introAnimation).setOnComplete(OnAnimationEnded);

        AudioManager.instance.PlayMenuSound(introSound);
        AudioManager.instance.PlayMusic(introMusic);
    }

    private void OnAnimationEnded()
    {
        overlayCanvas.SetActive(true);

    }
}
