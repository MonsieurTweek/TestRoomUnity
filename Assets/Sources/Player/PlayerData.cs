/// <summary>
/// Defines what a player is
/// </summary>
public class PlayerData : AbstractCharacterData
{
    public static readonly string TAG = "Player";

    public float energy { private set; get; }
    public float energyMax { protected set; get; }
    public float energyPerSecond { private set; get; }

    public float energyForDash { private set; get; }
    public float energyForLightAttack { private set; get; }
    public float energyForHeavyAttack { private set; get; }

    public uint playerAbilitiesMask { private set; get; }

    private int _tweenId = -1;
    private PlayerStateAttackConfiguration _stateAttackConfiguration = null;
    private Player _playerConfiguration = null;

    /// <summary>
    /// Populate model with data
    /// </summary>
    public override void Populate(Character configuration)
    {
        base.Populate(configuration);

        _playerConfiguration = ((Player)configuration);

        energy = _playerConfiguration.energy;
        energyMax = _playerConfiguration.energy;
        energyPerSecond = _playerConfiguration.energyPerSecond;

        energyForDash = _playerConfiguration.energyForDash;
        energyForLightAttack = _playerConfiguration.energyForLightAttack;
        energyForHeavyAttack = _playerConfiguration.energyForHeavyAttack;

        _stateAttackConfiguration = _playerConfiguration.attacksConfiguration;
    }

    public override void SetDamageByAttackType(CharacterStateAttack.AttackType type)
    {

        if (_stateAttackConfiguration == null)
        {
            damage = _playerConfiguration.damage;
            return;
        }

        try
        {
            UnityEngine.Assertions.Assert.IsTrue(_stateAttackConfiguration.damages.ContainsKey(type), "Can't find any damage for " + type);

            damage = _stateAttackConfiguration.damages[type];
        }
        catch (System.Exception e)
        {
            // The current AttackType doesn't have any override damage defined. Use the base damage value
            damage = _playerConfiguration.damage;
            return;
        }
    }

    public override int GetHealByAttackType(CharacterStateAttack.AttackType type)
    {
        if (_stateAttackConfiguration == null)
        {
            return 0;
        }

        try
        {
            UnityEngine.Assertions.Assert.IsTrue(_stateAttackConfiguration.heals.ContainsKey(type), "Can't find any heal for " + type);

            return _stateAttackConfiguration.heals[type];
        }
        catch (System.Exception e)
        {
            // The current AttackType doesn't have any heal defined
            return 0;
        }
    }

    public bool ConsumeEnergy(float amount)
    {
        if (energy - amount >= 0)
        {
            energy -= amount;

            if (LeanTween.isTweening(_tweenId) == true)
            {
                LeanTween.cancel(_tweenId);
            }

            _tweenId = LeanTween.value(energy, energyMax, (energyMax - energy) / energyPerSecond).setOnUpdate(UpdateEnergy).id;

            return true;
        }

        CharacterGameEvent.instance.OutOfEnergy(uniqueId);

        return false;
    }

    private void UpdateEnergy(float energy)
    {
        this.energy = energy;

        CharacterGameEvent.instance.UpdateEnergy(uniqueId, energy);
    }

    public void UnlockAbility(PlayerAbilityEnum playerAbility)
    {
        playerAbilitiesMask |= (uint)playerAbility;
    }
}