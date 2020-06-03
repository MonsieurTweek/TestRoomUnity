/// <summary>
/// Defines what an enemy is
/// </summary>
public class EnemyData : AbstractCharacterData
{
    public static readonly string TAG = "Enemy";

    /// <summary>
    /// Populate model with data
    /// </summary>
    public override void Populate()
    {
        // TODO : Use a scriptable object here
        health = 2;
    }
}