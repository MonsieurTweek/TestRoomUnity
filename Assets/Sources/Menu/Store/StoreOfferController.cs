using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreOfferController : MonoBehaviour
{
    public const string FREE_LABEL = "Free";

    [Header("References")]
    public Image icon = null;
    public TextMeshProUGUI titleText = null;
    public TextMeshProUGUI descriptionText = null;

    public TextMeshProUGUI priceText = null;
    public Image priceIcon = null;

    [Header("Properties")]
    public StoreOffer offer = null;

    private void Awake()
    {
        if (offer != null)
        {
            // Offer details
            titleText.text = offer.title;
            descriptionText.text = offer.description;
            icon.sprite = offer.icon;

            // Price
            priceText.text = offer.price > 0 ? offer.price.ToString() : FREE_LABEL;

            priceIcon.gameObject.SetActive(offer.price > 0);

            GetComponent<TileController>().onClick.AddListener(OnClick);
        }
    }

    private void OnClick()
    {
        offer.Buy();

        GameManager.instance.Save();
    }
}