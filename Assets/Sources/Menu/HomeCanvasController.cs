using UnityEngine;

public class HomeCanvasController : MonoBehaviour
{
    [Header("References")]
    public AnimatedPageController animatedPage = null;

    [Header("Properties")]
    public float animationTime = 0.5f;
    public float animationDelay = 0.5f;

    private void Start()
    {
        animatedPage.gameObject.SetActive(false);

        animatedPage.Show(animationTime, animationDelay);

        animatedPage.gameObject.SetActive(true);
    }
}