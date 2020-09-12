using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Archetype", menuName = "ScriptableObjects/Archetype")]
[Serializable]
public class Archetype : DescriptiveObject
{
    public static readonly string SAVE_PATH = "PLAYER_ARCHETYPE";

    public CharacterEnum character = CharacterEnum.NONE;
    public bool isPlayable = false;

    [Header("Configuration")]
    public Player configuration = null;
    public AnimatorOverrideController animationController = null;
    // TODO : A regrouper en un même objet options ?
    public PlayerStateAttackEffects attackEffects = null;
    public PlayerStateAttackStatus attackStatus = null;

    [Header("Gear")]
    public GearController gearLeft = null;
    public GearController gearRight = null;

    [Header("Ability description")]
    public string lightAbilityDescription = string.Empty;
    public string heavyAbilityDescription = string.Empty;
    public string passiveAbilityDescription = string.Empty;

    public void Save(string path)
    {
        PlayerPrefs.SetString(path, JsonUtility.ToJson(this));
    }

    public static Archetype Load(string path)
    {
        string serialized = PlayerPrefs.GetString(path);
        Archetype archetype = ScriptableObject.CreateInstance<Archetype>();

        JsonUtility.FromJsonOverwrite(serialized, archetype);

        return archetype;
    }
}