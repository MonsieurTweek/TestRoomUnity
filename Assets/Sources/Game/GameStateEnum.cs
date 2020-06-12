public enum GameStateEnum : uint
{
    NONE = 0,

    HOME            = 1 << 0,
    STORE           = 1 << 1,
    SELECTION       = 1 << 2,
    ARENA           = 1 << 3,
    UNLOCK_PERK     = 1 << 4
}