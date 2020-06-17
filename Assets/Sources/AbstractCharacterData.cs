using System;
/// <summary>
/// Defines generic rules for character
/// </summary>
public class AbstractCharacterData : AbstractUniqueData
{
    public int health { protected set; get; }
    public int healthMax { protected set; get; }
    public bool isAlive { get { return health > 0; } }

    public Action onBuffValues = null;
    
    /// <summary>
    /// Populate model with data
    /// </summary>
    public virtual void Populate()
    {
        // TODO : Use a scriptable object here
        healthMax = 10;
        health = healthMax;
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
        }

        if (onBuffValues != null)
        {
            onBuffValues();
        }
    }
}