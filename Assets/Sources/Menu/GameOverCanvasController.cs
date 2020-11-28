using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameOverCanvasController : MonoBehaviour
{
    private static readonly string ANALYTICS_UI_ELEMENT = "continue";

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

    public AudioClip[] rewardSounds = null;

    private int _tweenId = -1;
    private int _rewardAmount = 0;

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
    }

    private void StartRewardAnimation()
    {
        _rewardAmount = SaveData.current.playerProfile.currency - SaveData.current.playerProfile.lastCurrency;

        LeanTween.value(0, _rewardAmount, 0.7f).setOnUpdate(UpdateRewardText).setEase(counterEaseType).setOnComplete(DisplayCurrency);
    }

    private void UpdateRewardText(float amount)
    {
        counter.text = Mathf.RoundToInt(amount).ToString();

        if (amount > 0)
        {
            AudioManager.instance.PlayInGameSound(rewardSounds[Random.Range(0, rewardSounds.Length)]);
        }
    }

    private void DisplayCurrency()
    {
        LeanTween.scale(currency.gameObject, Vector3.one, 0.1f).setEase(currencyEaseType);

        // Bind input as the game over animation is done
        InputManager.instance.menu.Confirm.started += OnConfirmStarted;
        InputManager.instance.menu.Confirm.canceled += OnConfirmCanceled;
    }

    private void OnConfirmStarted(InputAction.CallbackContext context)
    {
        _tweenId = LeanTween.value(gameObject, 0f, 100f, confirmDelay).setOnUpdate(ConfirmProgress).setOnComplete(ConfirmComplete).id;

        AnalyticsManager.StartConfirmInput(ANALYTICS_UI_ELEMENT);
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
            AnalyticsManager.CancelConfirmInput(ANALYTICS_UI_ELEMENT, confirmBar.current);

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