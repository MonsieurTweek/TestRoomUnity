using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "ScriptableObjects/Character/Player")]
public class Player : Character
{
    public float energy = 3f;
    public float energyPerSecond = 1f;

    [Header("Energy costs")]
    public float energyForDash = 3f;
    public float energyForLightAttack = 1f;
    public float energyForHeavyAttack = 2f;

    [Header("Attacks")]
    public PlayerStateAttackConfiguration attacksConfiguration = null;
}