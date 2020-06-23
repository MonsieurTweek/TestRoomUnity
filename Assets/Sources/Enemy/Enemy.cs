using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/Character/Enemy")]
public class Enemy : Character
{
    public static readonly uint TIERS_MAX = 3;

    public uint tiers = 1;
}