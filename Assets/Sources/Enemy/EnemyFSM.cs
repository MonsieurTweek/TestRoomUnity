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
    public EnemyStateMove stateMove = new EnemyStateMove();
    public EnemyStateAttack stateAttack = new EnemyStateAttack();
    public EnemyStateHit stateHit = new EnemyStateHit();
    public EnemyStateDie stateDie = new EnemyStateDie();

    [Header("Properties")]
    public Character configuration = null;

    public Vector3 direction { private set; get; }

    // Transitions to states
    public override void TransitionToIdle() { ChangeState(stateIdle); }
    public void TransitionToIntro() { ChangeState(stateIntro); }
    public void TransitionToMove() { ChangeState(stateMove); }
    public void TransitionToAttack() { ChangeState(stateAttack, Random.Range(0, 2) == 1); } // Randomize ligh/heavy attack
    public void TransitionToAttackLight() { ChangeState(stateAttack, false); } // Forced light attack
    public void TransitionToAttackHeavy() { ChangeState(stateAttack, true); } // Forced heavy attack

    public void TransitionToHit() { ChangeState(stateHit); }
    public void TransitionToDie() { ChangeState(stateDie); }

    protected override void Awake()
    {
        base.Awake();

        stateIntro.flag = (uint)CharacterStateEnum.IDLE;
        stateIdle.flag = (uint)CharacterStateEnum.IDLE;
        stateMove.flag = (uint)CharacterStateEnum.MOVE;
        stateAttack.flag = (uint)CharacterStateEnum.ATTACK;
        stateHit.flag = (uint)CharacterStateEnum.HIT;
        stateDie.flag = (uint)CharacterStateEnum.DIE;

        data = new EnemyData();
    }

    public void Initialize(PlayerFSM player)
    {
        if (configuration != null)
        {
            data.Populate(configuration);
            target = player.transform;
        }
        else
        {
            Debug.LogError("Missing character configuration file for enemy " + name + " initialization");

            TransitionToDie();
        }
    }

    private void Start()
    {
        TransitionToIntro();
    }

    public override void Update()
    {
        if (target != null)
        {
            GetDirection();
        }

        base.Update();
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