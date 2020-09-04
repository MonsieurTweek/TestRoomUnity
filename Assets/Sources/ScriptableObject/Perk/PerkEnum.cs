public enum PerkEnum : uint
{
    NONE = 0,

    HEALTH = 1 << 0,
    ENERGY = 1 << 1,
    DASH = 1 << 2,
    STUN = 1 << 3,
    POISON = 1 << 4,
    FREEZE = 1 << 5,


    DEFAULT_UNLOCK = HEALTH | ENERGY
}