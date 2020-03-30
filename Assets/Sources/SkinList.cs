using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "SkinList", menuName = "Customization/Create Skin List", order = 1)]
public class SkinList : ScriptableObject
{
    public GameObject[] _skins = null;
}