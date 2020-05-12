using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Customization : ScriptableObject
{
    public bool isMale = true;
    public Material material = null;
    public SkinList skins = new SkinList();
    
    public void Populate(List<CustomizationPartOnPlayer> customizableParts)
    {
        foreach (CustomizationPartOnPlayer customizablePart in customizableParts)
        {
            skins.list.Add(customizablePart.skin);
        }
    }
}