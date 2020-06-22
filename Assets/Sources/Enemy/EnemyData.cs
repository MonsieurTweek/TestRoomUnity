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
        base.Populate();

        // TODO : Use a scriptable object here
        health = 3;
        power = 12;
    }
}