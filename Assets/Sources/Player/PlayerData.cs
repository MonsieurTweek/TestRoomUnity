/// <summary>
/// Defines what a player is
/// </summary>
public class PlayerData : AbstractCharacterData
{
    public static readonly string TAG = "Player";

    /// <summary>
    /// Populate model with data
    /// </summary>
    public override void Populate()
    {
        // TODO : Use a scriptable object here
        health = 1;
    }
}
