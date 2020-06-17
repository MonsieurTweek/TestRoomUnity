using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ProgressBarController : MonoBehaviour
{
    [Header("References")]
    public Image mask = null;
    public Image fill = null;
    public TextMeshProUGUI text = null;

    [Header("properties")]
    public int minimum = 0;
    public int maximum = 0;
    public int current = 0;
    public Color color = Color.magenta;
    public bool asPercentage = true;

    private void Awake()
    {
        fill.color = color;
    }

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

        text.text = asPercentage == true ? string.Format("{0}%", Mathf.Clamp(fillAmount * 100f, 0f, 100f)) : string.Format("{0}/{1}", current.ToString(), maximum.ToString());
    }
}