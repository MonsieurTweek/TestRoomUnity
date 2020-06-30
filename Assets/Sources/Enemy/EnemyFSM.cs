using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handle enemy behavior
/// </summary>
public class EnemyFSM : CharacterFSM, ICharacter
{
    private const uint FLAG_CAN_HIT = (uint)(CharacterStateEnum.IDLE | CharacterStateEnum.MOVE | CharacterStateEnum.ATTACK | CharacterStateEnum.STUN);

    [Header("References")]
    public Transform target = null;

    [Header("States")]
    public EnemyStateIntro stateIntro = new EnemyStateIntro();
    public EnemyStateReaction stateIdle = new EnemyStateReaction();
    public EnemyStateHit stateHit = new EnemyStateHit();
    public EnemyStateDie stateDie = new EnemyStateDie();

    [Header("States by Phases")]
    public List<EnemyPhase> phases = new List<EnemyPhase>();

    [Header("Properties")]
    public Character configuration = null;

    public int currentPhaseIndex { private set; get; }
    public Vector3 direction { private set; get; }

    // Phase action modifier
    public Action firstPhaseAttack = null;
    public Action secondPhaseAttack = null;

    // Transitions to states
    public override void TransitionToIdle() { ChangeState(stateIdle); }
    public void TransitionToIntro() { ChangeState(stateIntro); }
    public void TransitionToMove() { ChangeState(GetPhaseMove()); }
    public void TransitionToAttack() { ChangeState(GetPhaseAttack(), UnityEngine.Random.Range(0, 2) == 1); } // Randomize ligh/heavy attack
    public void TransitionToAttackLight() { ChangeState(GetPhaseAttack(), false); } // Forced light attack
    public void TransitionToAttackHeavy() { ChangeState(GetPhaseAttack(), true); } // Forced heavy attack

    public void TransitionToHit() { ChangeState(stateHit); }
    public void TransitionToDie() { ChangeState(stateDie); }

    protected override void Awake()
    {
        base.Awake();

        stateIntro.flag = (uint)CharacterStateEnum.INTRO;
        stateIdle.flag = (uint)CharacterStateEnum.IDLE;
        stateHit.flag = (uint)CharacterStateEnum.HIT;
        stateDie.flag = (uint)CharacterStateEnum.DIE;

        foreach(EnemyPhase phase in phases)
        {
            phase.stateMove.flag = (uint)CharacterStateEnum.MOVE;
            phase.stateAttack.flag = (uint)CharacterStateEnum.ATTACK;
        }

        data = new EnemyData();
    }

    public void Initialize(PlayerFSM player)
    {
        if (configuration != null)
        {
            data.Populate(configuration);
            target = player.transform;

            TransitionToIntro();
        }
        else
        {
            Debug.LogError("Missing character configuration file for enemy " + name + " initialization");

            TransitionToDie();
        }
    }

    public override void Update()
    {
        if (target != null)
        {
            GetDirection();
        }

        base.Update();
    }

    private EnemyStateMove GetPhaseMove()
    {
        return phases[currentPhaseIndex].stateMove;
    }

    private EnemyStateAttack GetPhaseAttack()
    {
        return phases[currentPhaseIndex].stateAttack;
    }

    /// <summary>
    /// Get the direction to the target
    /// </summary>
    private void GetDirection()
    {
        direction = transform.InverseTransformPoint(target.position);
    }

    /// <summary>
    /// Rotate the gameobject to look at the target
    /// </summary>
    public void LookAtTarget()
    {
        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

        transform.Rotate(0, angle, 0);
    }

    /// <summary>
    /// Apply damage to the enemy
    /// </summary>
    /// <param name="damage">Amount of damage. Reduction will be applied in this method.</param>
    /// <param name="isBlocking">Whether it should stop enemy (play animation) or not.</param>
    public bool Hit(int damage, bool isBlocking = true)
    {
        // Ensure player is in a state where he can take a hit
        if (((uint)currentState.flag & FLAG_CAN_HIT) != 0)
        {
            data.ApplyDamage(damage);

            UpdatePhase();

            CharacterGameEvent.instance.Hit(data, damage);

            if (data.isAlive == true)
            {
                if (isBlocking == true)
                {
                    TransitionToHit();
                }
            }
            else
            {
                CharacterGameEvent.instance.Die(data);
                TransitionToDie();
            }

            return true;
        }

        return false;
    }

    private void UpdatePhase()
    {
        // Check if damages taken so far are hitting the phase treshold
        if (data.healthMax - data.health >= phases[currentPhaseIndex].treshold)
        {
            currentPhaseIndex++;
        }
    }

    /// <summary>
    /// Animation triggers a FX
    /// Called from animation event
    /// </summary>
    public void OnAnimationPlayFx(AnimationEvent animationEvent)
    {
        if (currentState.flag == (uint)CharacterStateEnum.ATTACK)
        {
            ((EnemyStateAttack)currentState).OnAttackPlayFx(animationEvent.objectReferenceParameter, animationEvent.intParameter == 1);
        }
    }

    /// <summary>
    /// Animation triggers a FX
    /// Called from animation event
    /// </summary>
    public void OnAnimationSendProjectile(AnimationEvent animationEvent)
    {
        if (currentState.flag == (uint)CharacterStateEnum.ATTACK)
        {
            ((EnemyStateAttack)currentState).OnAttackSendProjectile(animationEvent.objectReferenceParameter, animationEvent.intParameter == 1);
        }
    }

    /// <summary>
    /// Called from animation event
    /// </summary>
    public void OnSingleAnimationEnded()
    {
        if (data.isAlive == true)
        {
            // Can't transition to something else while stun
            if (currentState.flag != (uint)CharacterStateEnum.STUN)
            {
                TransitionToIdle();
            }
        }
        else
        {
            currentState.Exit();

            GameObject.Destroy(gameObject);
        }
    }
}