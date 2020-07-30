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

    /// <summary>
    /// Populate model with data
    /// </summary>
    public override void Populate(Character configuration)
    {
        base.Populate(configuration);

        Player playerConfiguration = ((Player)configuration);

        energy = playerConfiguration.energy;
        energyMax = playerConfiguration.energy;
        energyPerSecond = playerConfiguration.energyPerSecond;

        energyForDash = playerConfiguration.energyForDash;
        energyForLightAttack = playerConfiguration.energyForLightAttack;
        energyForHeavyAttack = playerConfiguration.energyForHeavyAttack;
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