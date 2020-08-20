using System;
using UnityEngine;

[Serializable]
public class Character : ScriptableObject
{
    public CharacterTypeEnum type = CharacterTypeEnum.NONE;

    public string title = string.Empty;

    public int power = 1;

    public int health = 1;
}