using TMPro;
using UnityEngine;

public class PlayerUIController : MonoBehaviour
{
    [Header("References")]
    public PlayerFSM player = null;
    public GameObject layout = null;
    public HealthGaugeController gaugePlayer = null;
    public HealthGaugeController gaugeTarget = null;
    public PerkController[] perks = null;

    private void Awake()
    {
        for (int i = 0; i < perks.Length; i++)
        {
            perks[i].Initialize(i);
        }
    }

    private void Start()
    {
        CharacterGameEvent.instance.onHit += OnCharacterHit;
        CharacterGameEvent.instance.onTargetSelected += OnTargetSelected;
        CharacterGameEvent.instance.onTargetDeselected += OnTargetDeselected;

        CharacterGameEvent.instance.onIntroStarted += OnIntroStarted;
        CharacterGameEvent.instance.onIntroEnded += OnIntroEnded;

        player.data.onBuffValues += RefreshPlayerData;

        RefreshPlayerData();
    }

    private void OnIntroStarted(Transform target, AbstractCharacterData data)
    {
        layout.SetActive(false);
    }

    private void OnIntroEnded()
    {
        layout.SetActive(true);
    }

    private void RefreshPlayerData()
    {
        gaugePlayer.Refresh(player.data.health, player.data.healthMax);
    }

    private void OnCharacterHit(uint id, int health, int damage)
    {
        if (player.data.uniqueId == id)
        {
            gaugePlayer.Refresh(health, player.data.healthMax);
        }
        else if (player.target != null && player.target.data.uniqueId == id)
        {
            gaugeTarget.Refresh(health, player.target.data.healthMax);
        }
    }

    private void OnTargetSelected(uint id, int health, int healthMax)
    {
        gaugeTarget.Toggle(true);

        gaugeTarget.Refresh(health, healthMax);
    }

    private void OnTargetDeselected(uint id)
    {
        gaugeTarget.Toggle(false);
    }

    private void OnDestroy()
    {
        if (CharacterGameEvent.instance != null)
        {
            CharacterGameEvent.instance.onHit -= OnCharacterHit;
            CharacterGameEvent.instance.onTargetSelected -= OnTargetSelected;
            CharacterGameEvent.instance.onTargetDeselected -= OnTargetDeselected;

            CharacterGameEvent.instance.onIntroStarted -= OnIntroStarted;
            CharacterGameEvent.instance.onIntroEnded -= OnIntroEnded;
        }

        if (player != null && player.data != null)
        {
            player.data.onBuffValues -= RefreshPlayerData;
        }
    }
}