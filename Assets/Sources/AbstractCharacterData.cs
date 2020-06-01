/// <summary>
/// Defines generic rules for character
/// </summary>
public class AbstractCharacterData
{
    private static uint ID = 0;

    public uint uniqueId { private set; get; }
    public int health { private set; get; }
    public bool isAlive { get { return health > 0; } }

    public AbstractCharacterData()
    {
        uniqueId = ID++;
    }

    /// <summary>
    /// Populate model with data
    /// </summary>
    public virtual void Populate()
    {
        // TODO : Use a scriptable object here
        health = 10;
    }

    /// <summary>
    /// Apply final damages. No more computation here.
    /// </summary>
    /// <param name="damage">Amount of damage</param>
    public virtual void ApplyDamage(int damage)
    {
        health -= damage;
    }
}
