using TMPro;
using UnityEngine;

public class PlayerUIController : MonoBehaviour
{
    [Header("References")]
    public PlayerFSM player = null;
    public GameObject layout = null;
    public ResourceGaugeController playerDash = null;
    public ResourceGaugeController playerHealth = null;
    public ResourceGaugeController targetHealth = null;
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
        CharacterGameEvent.instance.onDashCompleted += OnCharacterDash;
        CharacterGameEvent.instance.onTargetSelected += OnTargetSelected;
        CharacterGameEvent.instance.onTargetDeselected += OnTargetDeselected;

        CharacterGameEvent.instance.onIntroStarted += OnIntroStarted;
        CharacterGameEvent.instance.onIntroEnded += OnIntroEnded;

        player.data.onBuffValues += RefreshPlayerData;

        RefreshPlayerData();

        playerDash.Refresh(100, 100);
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
        playerHealth.Refresh(player.data.health, player.data.healthMax);
    }

    private void OnCharacterDash(uint characterUniqueId, float cooldown)
    {
        if (player.data.uniqueId == characterUniqueId)
        {
            playerDash.Refresh(0, 100);

            LeanTween.value(0f, 100f, cooldown).setOnUpdate(DashCooldownProgress);
        }
    }

    private void DashCooldownProgress(float progress)
    {
        playerDash.Refresh(Mathf.RoundToInt(progress), 100);
    }

    private void OnCharacterHit(uint id, int health, int damage)
    {
        if (player.data.uniqueId == id)
        {
            playerHealth.Refresh(health, player.data.healthMax);
        }
        else if (player.target != null && player.target.data.uniqueId == id)
        {
            targetHealth.Refresh(health, player.target.data.healthMax);
        }
    }

    private void OnTargetSelected(uint id, int health, int healthMax)
    {
        targetHealth.Toggle(true);

        targetHealth.Refresh(health, healthMax);
    }

    private void OnTargetDeselected(uint id)
    {
        targetHealth.Toggle(false);
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