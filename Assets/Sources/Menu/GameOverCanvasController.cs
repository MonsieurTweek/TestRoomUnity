using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverCanvasController : MonoBehaviour
{
    [Header("References")]
    public CanvasGroup root = null;
    public TextMeshProUGUI title = null;
    public TextMeshProUGUI counter = null;
    public Image background = null;
    public Image currency = null;

    [Header("Properties")]
    public Color victoryColor = Color.black;
    public string victoryText = string.Empty;
    public Sprite victoryImage = null;

    public Color defeatColor = Color.black;
    public string defeatText = string.Empty;
    public Sprite defeatImage = null;

    public LeanTweenType counterEaseType = LeanTweenType.linear;
    public LeanTweenType currencyEaseType = LeanTweenType.linear;

    private void Awake()
    {
        root.alpha = 0f;
        counter.text = string.Empty;
        currency.transform.localScale = Vector3.zero;
    }

    private void Start()
    {
        GameStateGameOver state = (GameStateGameOver)GameManager.instance.GetCurrentState();

        if (state != null)
        {
            title.text = state.hasWon ? victoryText : defeatText;
            title.color = state.hasWon ? victoryColor : defeatColor;
            background.sprite = state.hasWon ? victoryImage : defeatImage;
        }

        LeanTween.alphaCanvas(root, 1f, 1f).setOnComplete(StartRewardAnimation);
    }

    private void StartRewardAnimation()
    {
        int amount = SaveData.current.playerProfile.currency - SaveData.current.playerProfile.lastCurrency;

        LeanTween.value(0, amount, 0.5f).setOnUpdate(UpdateRewardText).setEase(counterEaseType).setOnComplete(DisplayCurrency);
    }

    private void UpdateRewardText(float amount)
    {
        counter.text = Mathf.RoundToInt(amount).ToString();
    }

    private void DisplayCurrency()
    {
        LeanTween.scale(currency.gameObject, Vector3.one, 0.1f).setEase(currencyEaseType);
    }
}