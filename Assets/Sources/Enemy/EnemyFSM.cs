using UnityEngine;

/// <summary>
/// Handle enemy behavior
/// </summary>
public class EnemyFSM : CharacterFSM, ICharacter
{
    private const uint FLAG_CAN_HIT = (uint)(CharacterStateEnum.IDLE | CharacterStateEnum.MOVE | CharacterStateEnum.ATTACK);

    [Header("References")]
    public Transform target = null;

    [Header("States")]
    public EnemyStateReaction stateIdle = new EnemyStateReaction();
    public EnemyStateMove stateMove = new EnemyStateMove();
    public EnemyStateAttack stateAttack = new EnemyStateAttack();
    public EnemyStateHit stateHit = new EnemyStateHit();
    public EnemyStateDie stateDie = new EnemyStateDie();

    public Vector3 direction { private set; get; }

    // Transitions to states
    public void TransitionToIdle() { ChangeState(stateIdle, TransitionToMove); }
    public void TransitionToMove() { ChangeState(stateMove, TransitionToAttack); }
    public void TransitionToAttack() { ChangeState(stateAttack, Random.Range(0, 2) == 1); } // Randomize ligh/heavy attack

    public void TransitionToHit() { ChangeState(stateHit); }
    public void TransitionToDie() { ChangeState(stateDie); }

    private void Awake()
    {
        stateIdle.flag = (uint)CharacterStateEnum.IDLE;
        stateMove.flag = (uint)CharacterStateEnum.MOVE;
        stateAttack.flag = (uint)CharacterStateEnum.ATTACK;
        stateHit.flag = (uint)CharacterStateEnum.HIT;
        stateDie.flag = (uint)CharacterStateEnum.DIE;
    }

    public void Initialize(PlayerFSM player)
    {
        data = new EnemyData();
        data.Populate();

        target = player.transform;
    }

    private void Start()
    {
        TransitionToIdle();
    }

    public override void Update()
    {
        GetDirection();

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
    public void Hit(int damage)
    {
        // Ensure player is in a state where he can take a hit
        if (((uint)currentState.flag & FLAG_CAN_HIT) != 0)
        {
            data.ApplyDamage(damage);

            CharacterGameEvent.instance.HitRaised(data, damage);

            if (data.isAlive == true)
            {
                TransitionToHit();
            }
            else
            {
                TransitionToDie();
            }
        }
    }

    /// <summary>
    /// Called from animation event
    /// </summary>
    public void OnSingleAnimationEnded()
    {
        if (data.isAlive == true)
        {
            TransitionToIdle();
        }
        else
        {
            currentState.Exit();

            GameObject.Destroy(gameObject);
        }
    }
}