using UnityEngine;

public class HealthGaugeController : MonoBehaviour
{
    [Header("References")]
    public ProgressBarController gauge = null;

    [Header("Properties")]
    public bool isActiveOnAwake = true;

    private void Awake()
    {
        gauge.gameObject.SetActive(isActiveOnAwake);
    }

    public void Refresh(int current, int maximum)
    {
        gauge.current = current >= gauge.minimum ? current : 0;
        gauge.maximum = maximum;
    }

    public void Toggle(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}
