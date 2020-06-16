using TMPro;
using UnityEngine;

public class PlayerProfileController : MonoBehaviour
{
    [Header("References")]
    public TextMeshProUGUI currency = null;

    private void Start()
    {
        GameEvent.instance.onDataSaved += RefreshUI;

        RefreshUI();
    }

    private void RefreshUI()
    {
        currency.text = SaveData.current.playerProfile.currency.ToString();
    }

    private void OnDestroy()
    {
        if (GameEvent.instance != null)
        {
            GameEvent.instance.onDataSaved -= RefreshUI;
        }
    }
}