/// <summary>
/// Defines what an enemy is
/// </summary>
public class EnemyData : AbstractCharacterData
{
    public static readonly string TAG = "Enemy";

    public uint tiers = 1;

    /// <summary>
    /// Populate model with data
    /// </summary>
    public override void Populate(Character configuration)
    {
        base.Populate(configuration);

        Enemy enemy = (Enemy)configuration;

        tiers = enemy.tiers;
    }
}