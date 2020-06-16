using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreOfferController : MonoBehaviour
{
    [Header("References")]
    public TextMeshProUGUI priceText = null;
    public TextMeshProUGUI titleText = null;
    public TextMeshProUGUI descriptionText = null;
    public Image icon = null;

    [Header("Properties")]
    public StoreOffer offer = null;

    private void Awake()
    {
        if (offer != null)
        {
            priceText.text = offer.price.ToString();
            titleText.text = offer.title;
            descriptionText.text = offer.description;

            icon.sprite = offer.icon;
        }
    }
}