/// <summary>
/// Defines what an enemy is
/// </summary>
public class EnemyData
{
    public int health { private set; get; }
    public bool isAlive { get { return health > 0; } }

    /// <summary>
    /// Populate model with data
    /// </summary>
    public void Populate()
    {
        // TODO : Use a scriptable object here
        health = 10;
    }

    /// <summary>
    /// Apply final damages. No more computation here.
    /// </summary>
    /// <param name="damage">Amount of damage</param>
    public void ApplyDamage(int damage)
    {
        health -= damage;
    }
}