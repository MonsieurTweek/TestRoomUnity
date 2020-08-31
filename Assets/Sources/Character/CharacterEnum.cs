public enum CharacterEnum : uint
{
    NONE = 0,

    ASSASSIN = 1 << 0,
    BARBARIAN = 1 << 1,
    HUNTER = 1 << 2,
    PALADIN = 1 << 3,
    PRIEST = 1 << 4,
    ROGUE = 1 << 5,
    SORCERER = 1 << 6,
    WARLOCK = 1 << 7,
    WARRIOR = 1 << 8,


    DEFAULT_UNLOCK = SORCERER | WARRIOR

}
