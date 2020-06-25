using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PoisonStatus", menuName = "ScriptableObjects/Status/Poison")]
[Serializable]
public class PoisonStatus : Status
{
    public int damage = 1;

    public override void Evaluate(CharacterFSM character)
    {
        EnemyFSM enemy = (EnemyFSM)character;

        if (enemy != null)
        {
            enemy.Hit(damage, false);
        }
    }
}