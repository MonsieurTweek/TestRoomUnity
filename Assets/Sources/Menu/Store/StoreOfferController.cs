using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreOfferController : MonoBehaviour
{
    public const string FREE_LABEL = "Free";
    public const string PURCHASED_LABEL = "Purchased";

    [Header("References")]
    public Image icon = null;
    public Image background = null;
    public TextMeshProUGUI titleText = null;
    public TextMeshProUGUI descriptionText = null;

    public RectTransform priceLayout = null;
    public TextMeshProUGUI priceText = null;
    public Image priceIcon = null;

    [Header("Properties")]
    public StoreOffer offer = null;

    private TileController _tile = null;

    private void Awake()
    {
        _tile = GetComponent<TileController>();

        if (offer != null)
        {
            // Offer details
            titleText.text = offer.title;
            descriptionText.text = offer.description;
            icon.sprite = offer.icon;
            background.sprite = offer.background;

            // Price and confirmation
            Refresh();
        }
    }

    private void Refresh()
    {
        if (IsPurchased() == false)
        {
            priceText.text = offer.price > 0 ? offer.price.ToString() : FREE_LABEL;

            priceIcon.gameObject.SetActive(offer.price > 0);

            _tile.onClick.AddListener(OnClick);
        }
        else
        {
            priceText.text = PURCHASED_LABEL;
            priceIcon.gameObject.SetActive(false);

            _tile.DisableConfirmation();
        }
    }

    private bool IsPurchased()
    {
        if (offer is CharacterOffer)
        {
            return (SaveData.current.playerProfile.characters & (uint)((CharacterOffer)offer).character) != 0;
        }
        else if (offer is PerkOffer)
        {
            return (SaveData.current.playerProfile.perks & (uint)((PerkOffer)offer).perk) != 0;
        }

        return false;
    }

    private void OnClick()
    {
        if (IsPurchased() == true || offer.price > SaveData.current.playerProfile.currency)
        {
            AudioManager.instance.PlayMenuSound(AudioManager.instance.menuConfirmationFailedSfx);

            AnimatePrice();
        }
        else
        {
            offer.Buy();

            AudioManager.instance.PlayInGameSound(offer.purchaseSound);

            GameManager.instance.Save();

            Refresh();
        }
    }

    private void AnimatePrice()
    {
        LeanTween.scale(priceLayout, Vector3.one * 1.5f, 0.25f).setEase(LeanTweenType.easeInOutBounce).setLoopPingPong(1);
        LeanTween.value(priceText.gameObject, priceText.color, Color.red, 0.25f).setOnUpdateColor(OnUpdatePriceColor).setLoopPingPong(1);
    }
    
    private void OnUpdatePriceColor(Color color)
    {
        priceText.color = color;
    }
}