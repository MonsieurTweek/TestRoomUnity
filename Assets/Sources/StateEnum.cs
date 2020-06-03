public enum StateEnum : uint
{
    NONE = 0,

    IDLE    = 1 << 0,
    MOVE    = 1 << 1,
    ATTACK  = 1 << 2,
    HIT     = 1 << 3,
    DIE     = 1 << 4
}