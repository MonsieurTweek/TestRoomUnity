using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Skin
{
    public string name = string.Empty;
    public uint index = 0;
    public Material material = null;

    public override string ToString()
    {
        return "Part [" + name + "] with index " + index + ".";
    }
}

[Serializable]
public class SkinList
{
    public List<Skin> list = new List<Skin>();
}
