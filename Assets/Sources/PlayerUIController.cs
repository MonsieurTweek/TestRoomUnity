using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    [Header("References")]
    public PlayerFSM player = null;
    public Image gaugeHealth = null;
    public Image gaugeHealthTarget = null;

    private void Awake()
    {
        gaugeHealthTarget.enabled = false;
    }

    private void Start()
    {
        CharacterGameEvent.instance.onHit += OnCharacterHit;
        CharacterGameEvent.instance.onTargetSelected += OnTargetSelected;
        CharacterGameEvent.instance.onTargetDeselected += OnTargetDeselected;
    }

    private void OnCharacterHit(uint id, int health, int damage)
    {
        if (player.data.uniqueId == id)
        {
            gaugeHealth.fillAmount = health / 10f;
        }
        else if (player.target != null && player.target.data.uniqueId == id)
        {
            gaugeHealthTarget.fillAmount = health / 10f;
        }
    }

    private void OnTargetSelected(uint id, int health)
    {
        gaugeHealthTarget.enabled = true;

        gaugeHealthTarget.fillAmount = health / 10f;
    }

    private void OnTargetDeselected(uint id)
    {
        gaugeHealthTarget.enabled = false;
    }

    private void OnDestroy()
    {
        CharacterGameEvent.instance.onHit -= OnCharacterHit;
        CharacterGameEvent.instance.onTargetSelected -= OnTargetSelected;
        CharacterGameEvent.instance.onTargetDeselected -= OnTargetDeselected;
    }
}