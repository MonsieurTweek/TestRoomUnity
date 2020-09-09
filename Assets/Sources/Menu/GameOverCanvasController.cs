using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameOverCanvasController : MonoBehaviour
{
    [Header("References")]
    public CanvasGroup root = null;
    public TextMeshProUGUI title = null;
    public TextMeshProUGUI counter = null;
    public Image background = null;
    public Image currency = null;
    public ProgressBarController confirmBar = null;
    public GameStatisticController statisticController = null;

    [Header("Properties")]
    public Color victoryColor = Color.black;
    public string victoryText = string.Empty;
    public Sprite victoryImage = null;
    public AudioClip victorySfx = null;

    public Color defeatColor = Color.black;
    public string defeatText = string.Empty;
    public Sprite defeatImage = null;
    public AudioClip defeatSfx = null;

    public LeanTweenType counterEaseType = LeanTweenType.linear;
    public LeanTweenType currencyEaseType = LeanTweenType.linear;

    public float confirmDelay = 0.25f;

    private int _tweenId = -1;

    private void Awake()
    {
        root.alpha = 0f;
        counter.text = string.Empty;
        currency.transform.localScale = Vector3.zero;
    }

    private void Start()
    {
        CharacterGameEvent.instance.onOutroPlaying += OnOutroPlaying;
    }

    private void OnOutroPlaying()
    {
        statisticController.RefreshForSession();

        confirmBar.current = 0;

        title.text = SaveData.current.playerProfile.lastMatchWon ? victoryText : defeatText;
        title.color = SaveData.current.playerProfile.lastMatchWon ? victoryColor : defeatColor;
        background.sprite = SaveData.current.playerProfile.lastMatchWon ? victoryImage : defeatImage;

        LeanTween.alphaCanvas(root, 1f, 1f).setOnComplete(StartRewardAnimation);

        AudioManager.instance.FadeOutMusic();
        AudioManager.instance.PlayMenuSound(SaveData.current.playerProfile.lastMatchWon ? victorySfx : defeatSfx);

        InputManager.instance.menu.Confirm.started += OnConfirmStarted;
        InputManager.instance.menu.Confirm.canceled += OnConfirmCanceled;
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

    private void OnConfirmStarted(InputAction.CallbackContext context)
    {
        _tweenId = LeanTween.value(gameObject, 0f, 100f, confirmDelay).setOnUpdate(ConfirmProgress).setOnComplete(ConfirmComplete).id;
    }

    private void ConfirmProgress(float progress)
    {
        confirmBar.current = Mathf.RoundToInt(progress);
    }

    private void ConfirmComplete()
    {
        AudioManager.instance.PlayMenuSound(AudioManager.instance.menuConfirmationSfx);

        GameEvent.instance.HomeButtonPressed();

        AudioManager.instance.FadeInMusic();
    }

    private void OnConfirmCanceled(InputAction.CallbackContext context)
    {
        if (LeanTween.isTweening(_tweenId) == true)
        {
            LeanTween.cancel(gameObject, _tweenId);

            confirmBar.current = 0;
        }
    }

    private void OnDestroy()
    {
        if (CharacterGameEvent.instance != null)
        {
            CharacterGameEvent.instance.onOutroPlaying -= OnOutroPlaying;
        }

        if (InputManager.instance != null)
        {
            InputManager.instance.menu.Confirm.started -= OnConfirmStarted;
            InputManager.instance.menu.Confirm.canceled -= OnConfirmCanceled;
        }
    }
}