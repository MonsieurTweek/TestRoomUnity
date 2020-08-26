using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Defines what to do when enemy attacks
/// </summary>
[Serializable]
public class EnemyStateAttack : CharacterStateAttack
{
    public List<VisualEffectController> effects = new List<VisualEffectController>();

    private int _currentFxIndex = 0;

    public override void Enter(int type)
    {
        // Try to resync orientation with player just before attacking
        ((EnemyFSM)character).LookAtTarget(10f);

        character.animator.applyRootMotion = true;

        _currentFxIndex = 0;

        base.Enter(type);
    }

    public GameObject OnAttackSpawnMinion(UnityEngine.Object prefab, float offset)
    {
        GameObject minion = GamePoolManager.instance.UseFromPool(prefab.name);
        Transform target = ((EnemyFSM)character).target;

        minion.transform.position = target.transform.position + target.transform.forward * offset;
        minion.transform.rotation = character.transform.rotation;

        minion.SetActive(true);

        EnemyFSM enemy = minion.GetComponent<EnemyFSM>();
        PlayerFSM player = target.GetComponent<PlayerFSM>();

        enemy.Initialize(player);

        return minion;
    }

    public void OnAttackPlayFx()
    {
        if (_currentFxIndex < effects.Count)
        {
            effects[_currentFxIndex].Reset(flag);

            _currentFxIndex++;
        }

        if (_currentFxIndex >= effects.Count)
        {
            _currentFxIndex = 0;
        }
    }

    public override void Exit()
    {
        base.Exit();

        // After each attack evaluate a new phase
        ((EnemyFSM)character).EvaluateNextPhase();
    }
}