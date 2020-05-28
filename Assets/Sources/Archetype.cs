using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Archetype", menuName = "ScriptableObjects/Archetype", order = 1)]
[Serializable]
public class Archetype : ScriptableObject
{
    public string title = string.Empty;
    public string description = string.Empty;
    public Sprite icon = null;

    public AnimatorOverrideController animationController = null;

    public GearController gearLeft = null;
    public GearController gearRight = null;
}
