using System;

/// <summary>
/// Defines generic rules for character
/// </summary>
public class AbstractCharacterData : AbstractUniqueData
{
    public string name { protected set; get; }

    public CharacterTypeEnum type = CharacterTypeEnum.NONE;

    public int power { protected set; get; }
    public int health { protected set; get; }
    public int healthMax { protected set; get; }
    public int damage { protected set; get; }
    public bool isAlive { get { return health > 0; } }

    public uint status { protected set; get; }

    public Action onBuffValues = null;
    
    /// <summary>
    /// Populate model with data
    /// </summary>
    public virtual void Populate(Character configuration)
    {
        type = configuration.type;
        name = configuration.title;
        power = configuration.power;
        healthMax = configuration.health;
        health = healthMax;
        damage = configuration.damage;
    }

    /// <summary>
    /// Apply final damages. No more computation here.
    /// </summary>
    /// <param name="damage">Amount of damage</param>
    public virtual void ApplyDamage(int damage)
    {
        health -= damage;
    }

    public void BuffHealth(int amount, bool isPermanent)
    {
        health += amount;

        if (isPermanent == true)
        {
            healthMax += amount;
        } else
        {
            health = Math.Min(health, healthMax);
        }

        if (onBuffValues != null)
        {
            onBuffValues();
        }
    }

    public void SetStatus(CharacterStatusEnum type)
    {
        status |= (uint)type;
    }

    public void RemoveStatus(CharacterStatusEnum type)
    {
        status &= ~(uint)type;
    }

    public bool HasStatus(CharacterStatusEnum type)
    {
        return (status & (uint)type) != 0;
    }

    public virtual int GetReward()
    {
        return power;
    }
}