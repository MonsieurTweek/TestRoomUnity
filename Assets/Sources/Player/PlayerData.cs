/// <summary>
/// Defines what a player is
/// </summary>
public class PlayerData : AbstractCharacterData
{
    public static readonly string TAG = "Player";

    public float dashCooldown{ private set; get; }

    /// <summary>
    /// Populate model with data
    /// </summary>
    public override void Populate(Character configuration)
    {
        base.Populate(configuration);

        dashCooldown = ((Player)configuration).dashCooldown;
    }
}
