using UnityEngine;

/// <summary>
/// Handle enemy behavior
/// </summary>
public class EnemyFSM : CharacterFSM, ICharacter
{
    [Header("References")]
    public Transform target = null;
    public Animator animator = null;

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
    public void TransitionToAttack() { ChangeState(stateAttack); }

    public void TransitionToHit() { ChangeState(stateHit); }
    public void TransitionToDie() { ChangeState(stateDie); }

    private void Awake()
    {
        data = new EnemyData();
        data.Populate();

        stateIdle.flag = StateEnum.IDLE;
        stateMove.flag = StateEnum.MOVE;
        stateAttack.flag = StateEnum.ATTACK;
        stateHit.flag = StateEnum.HIT;
        stateDie.flag = StateEnum.DIE;
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
            GameObject.Destroy(gameObject);
        }
    }
}