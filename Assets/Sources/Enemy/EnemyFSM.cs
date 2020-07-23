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
    public EnemyStateDash stateDash = new EnemyStateDash();
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
    public void TransitionToDash() { ChangeState(stateDash); }
    public void TransitionToAttack() { ChangeState(GetPhaseAttack(), (int)GetPhaseAttack().type); }

    public void TransitionToHit() { ChangeState(stateHit); }
    public void TransitionToDie() { ChangeState(stateDie); }

    private List<int> _availablePhases = new List<int>();

    protected override void Awake()
    {
        base.Awake();

        stateIntro.flag = (uint)CharacterStateEnum.INTRO;
        stateIdle.flag = (uint)CharacterStateEnum.IDLE;
        stateHit.flag = (uint)CharacterStateEnum.HIT;
        stateDie.flag = (uint)CharacterStateEnum.DIE;
        stateDash.flag = (uint)CharacterStateEnum.MOVE;

        data = new EnemyData();
    }

    public void Initialize(PlayerFSM player)
    {
        if (configuration != null)
        {
            data.Populate(configuration);
            target = player.transform;

            _availablePhases.Clear();

            for (int i = 0; i < phases.Count; i++)
            {
                phases[i].stateMove.flag = (uint)CharacterStateEnum.MOVE;
                phases[i].stateAttack.flag = (uint)CharacterStateEnum.ATTACK;

                if (phases[i].IsAvailable(data) == true)
                {
                    _availablePhases.Add(i);
                }

            }

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

            CharacterGameEvent.instance.Hit(data, damage);

            if (data.isAlive == true)
            {
                UpdatePhases();

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

    public void EvaluateNextPhase()
    {
        int index = UnityEngine.Random.Range(0, _availablePhases.Count);

        currentPhaseIndex = _availablePhases[index];
    }

    private void UpdatePhases()
    {
        List<int> toAdd = new List<int>();
        List<int> toRemove = new List<int>(); 

        // Check if damages taken so far are hitting the phase treshold
        for (int i = 0; i < phases.Count; i++)
        {
            bool isAvailable = phases[i].IsAvailable(data);
            bool isAlreadyAvailable = _availablePhases.Contains(i);

            // Add the phase only if we need to
            if (isAvailable == true && isAlreadyAvailable == false)
            {
                toAdd.Add(i);
            }
            // Remove it if was available but not anymore
            else if (isAvailable == false && isAlreadyAvailable == true)
            {
                toRemove.Add(i);
            }
        }

        // Remove all phases which don't fit anymore
        for (int i = 0; i < toRemove.Count; i++)
        {
            _availablePhases.Remove(toRemove[i]);
        }

        // Add new phases availables
        for (int i = 0; i < toAdd.Count; i++)
        {
            _availablePhases.Add(toAdd[i]);
        }

        EvaluateNextPhase();
    }

    /// <summary>
    /// Animation triggers the spawn of a projectile
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
    /// Animation triggers the spawn of a minion
    /// Called from animation event
    /// </summary>
    public void OnAnimationSpawnMinion(AnimationEvent animationEvent)
    {
        if (currentState.flag == (uint)CharacterStateEnum.ATTACK)
        {
            ((EnemyStateAttack)currentState).OnAttackSpawnMinion(animationEvent.objectReferenceParameter, animationEvent.floatParameter);
        }
    }

    /// <summary>
    /// Animation event triggers a change of animation speed
    /// </summary>
    public void OnUpdateAttackSpeed(AnimationEvent animationEvent)
    {
        if (currentState.flag == (uint)CharacterStateEnum.ATTACK)
        {
            ((EnemyStateAttack)currentState).OnUpdateAttackSpeed(animationEvent.floatParameter);
        }
    }

    /// <summary>
    /// Animation event triggers a predifined FX to play based on the attack
    /// </summary>
    public void OnAttackPlayFx()
    {
        if (currentState.flag == (uint)CharacterStateEnum.ATTACK)
        {
            ((EnemyStateAttack)currentState).OnAttackPlayFx();
        }
    }

    /// <summary>
    /// Called from animation event
    /// </summary>
    public virtual void OnSingleAnimationEnded()
    {
        if (data.isAlive == true)
        {
            CharacterStateBase state = (CharacterStateBase)currentState;

            state.OnSingleAnimationEnded();
        }
        else
        {
            currentState.Exit();

            GameObject.Destroy(gameObject);
        }
    }
}