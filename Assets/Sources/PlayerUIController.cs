using UnityEngine;

public class PlayerUIController : MonoBehaviour
{
    [Header("References")]
    public PlayerFSM player = null;
    public ProgressBarController gaugeHealth = null;
    public ProgressBarController gaugeHealthTarget = null;

    private void Awake()
    {
        gaugeHealthTarget.gameObject.SetActive(false);
    }

    private void Start()
    {
        CharacterGameEvent.instance.onHit += OnCharacterHit;
        CharacterGameEvent.instance.onTargetSelected += OnTargetSelected;
        CharacterGameEvent.instance.onTargetDeselected += OnTargetDeselected;

        player.data.onBuffValues += RefreshPlayerData;

        RefreshPlayerData();
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

    private void OnDestroy()
    {
        if (CharacterGameEvent.instance != null)
        {
            CharacterGameEvent.instance.onHit -= OnCharacterHit;
            CharacterGameEvent.instance.onTargetSelected -= OnTargetSelected;
            CharacterGameEvent.instance.onTargetDeselected -= OnTargetDeselected;
        }

        if (player != null && player.data != null)
        {
            player.data.onBuffValues -= RefreshPlayerData;
        }
    }
}