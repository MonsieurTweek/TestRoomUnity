public enum CharacterStateEnum : uint
{
    NONE = 0,

    IDLE    = 1 << 0,
    MOVE    = 1 << 1,
    ATTACK  = 1 << 2,
    HIT     = 1 << 3,
    DIE     = 1 << 4,
    STUN    = 1 << 5,
    INTRO   = 1 << 6,
    DASH    = 1 << 7
}