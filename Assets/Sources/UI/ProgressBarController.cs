using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ProgressBarController : MonoBehaviour
{
    public int minimum = 0;
    public int maximum = 0;
    public int current = 0;
    public Image mask = null;
    public Image fill = null;
    public Color color = Color.magenta;

    private void Update()
    {
        GetCurrentFill();
    }

    private void GetCurrentFill()
    {
        float currentOffset = current - minimum;
        float maximumOffset = maximum - minimum;
        float fillAmount = currentOffset / maximumOffset;

        mask.fillAmount = fillAmount;
    }
}