using System;
using UnityEngine;

[Serializable]
public class DescriptiveObject : ScriptableObject
{
    public string title = string.Empty;
    public string description = string.Empty;
    public Sprite icon = null;
    public Sprite background = null;
}