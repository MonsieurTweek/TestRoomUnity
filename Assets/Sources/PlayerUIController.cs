using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    [Header("References")]
    public PlayerFSM player = null;
    public ProgressBarController gaugeHealth = null;
    public ProgressBarController gaugeHealthTarget = null;
    public Transform introLayout = null;
    public TextMeshProUGUI introTitle = null;
    public FlexibleGridLayout perkLayout = null;

    [Header("Properties")]
    public Image perkAsBattleIcon = null;

    private Vector3 _introOriginalPosition = Vector3.zero;

    private void Awake()
    {
        gaugeHealthTarget.gameObject.SetActive(false);

        _introOriginalPosition = introLayout.transform.position;
    }

    private void Start()
    {
        CharacterGameEvent.instance.onHit += OnCharacterHit;
        CharacterGameEvent.instance.onTargetSelected += OnTargetSelected;
        CharacterGameEvent.instance.onTargetDeselected += OnTargetDeselected;

        CharacterGameEvent.instance.onIntroStarted += OnIntroStarted;
        CharacterGameEvent.instance.onIntroEnded += OnIntroEnded;

        PerkGameEvent.instance.onUnlocked += OnPerkUnlocked;

        player.data.onBuffValues += RefreshPlayerData;

        RefreshPlayerData();
    }

    private void OnIntroStarted(Transform target, AbstractCharacterData data)
    {
        introTitle.text = data.name;

        // Position is reset to be out of screen
        introLayout.transform.position = new Vector3(-Screen.width, _introOriginalPosition.y, 0f);

        LeanTween.moveX(introLayout.gameObject, Screen.width * 0.5f, 1f).setEase(LeanTweenType.easeOutElastic).setDelay(0.1f);

        introLayout.gameObject.SetActive(true);
        gaugeHealth.gameObject.SetActive(false);
    }

    private void OnIntroEnded()
    {
        gaugeHealth.gameObject.SetActive(true);
        introLayout.gameObject.SetActive(false);
    }

    private void RefreshPlayerData()
    {
        gaugeHealth.current = player.data.health;
        gaugeHealth.maximum = player.data.healthMax;
    }

    private void OnCharacterHit(uint id, int health, int damage)
    {
        if (player.data.uniqueId == id)
        {
            gaugeHealth.current = health;
        }
        else if (player.target != null && player.target.data.uniqueId == id)
        {
            gaugeHealthTarget.current = health;
        }
    }

    private void OnTargetSelected(uint id, int health, int healthMax)
    {
        gaugeHealthTarget.gameObject.SetActive(true);

        gaugeHealthTarget.current = health;
        gaugeHealthTarget.maximum = healthMax;
    }

    private void OnTargetDeselected(uint id)
    {
        gaugeHealthTarget.gameObject.SetActive(false);
    }

    private void OnPerkUnlocked(uint uniqueId, Perk perk)
    {
        Image newPerk = Instantiate<Image>(perkAsBattleIcon, perkLayout.transform);

        newPerk.sprite = perk.icon;
    }

    private void OnDestroy()
    {
        if (CharacterGameEvent.instance != null)
        {
            CharacterGameEvent.instance.onHit -= OnCharacterHit;
            CharacterGameEvent.instance.onTargetSelected -= OnTargetSelected;
            CharacterGameEvent.instance.onTargetDeselected -= OnTargetDeselected;
        }

        if (PerkGameEvent.instance != null)
        {
            PerkGameEvent.instance.onUnlocked -= OnPerkUnlocked;
        }

        if (player != null && player.data != null)
        {
            player.data.onBuffValues -= RefreshPlayerData;
        }
    }
}