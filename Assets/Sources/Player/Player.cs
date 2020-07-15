using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "ScriptableObjects/Character/Player")]
public class Player : Character
{
    public float dashCooldown = 3f;
}