using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFSM : CharacterFSM, ICharacter
{
    private const uint FLAG_CAN_ATTACK = (uint)StateEnum.MOVE;
    private const uint FLAG_CAN_HIT = (uint)(StateEnum.MOVE | StateEnum.ATTACK);

    [Header("References")]
    public Animator animator = null;
    public PlayerCameraController cameraController = null;

    [Header("States")]
    public PlayerStateMove stateMove = new PlayerStateMove();
    public PlayerStateAttack stateAttack = new PlayerStateAttack();
    public PlayerStateHit stateHit = new PlayerStateHit();
    public PlayerStateDie stateDie = new PlayerStateDie();

    [Header("Properties")]
    public float rotationSpeed = 1f;
    public float rangeForTarget = 15f;
    private Vector2 _playerRotation = Vector2.zero;

    // Transitions to states
    public void TransitionToMove() { ChangeState(stateMove); }
    public void TransitionToAttack() { ChangeState(stateAttack); }
    public void TransitionToHit() { ChangeState(stateHit); }
    public void TransitionToDie() { ChangeState(stateDie); }

    public CharacterFSM target { private set; get; }

    private void Awake()
    {
        data = new PlayerData();
        data.Populate();

        stateMove.flag = StateEnum.MOVE;
        stateAttack.flag = StateEnum.ATTACK;
        stateHit.flag = StateEnum.HIT;
        stateDie.flag = StateEnum.DIE;

        // Save current player rotation to start with
        _playerRotation = transform.eulerAngles;
    }

    private void Start()
    {
        TransitionToMove();
    }

    public override void Update()
    {
        base.Update();

        // Attack
        if (Input.GetMouseButtonUp(0) == true)
        {
            TransitionToAttack();
        }

        // Toggle target mode
        if (Input.GetKeyUp(KeyCode.LeftControl) == true)
        {
            if (target == null)
            {
                AcquireTarget();
            }
            else
            {
                ReleaseTarget();
            }
        }
    }
    
    private GameObject[] FindTargetInRange()
    {
        List<GameObject> potentialTargets = new List<GameObject>();
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, rangeForTarget);

        int i = 0;

        while (i < hitColliders.Length)
        {
            if (hitColliders[i].tag == EnemyData.TAG)
            {
                potentialTargets.Add(hitColliders[i].gameObject);
            }
            i++;
        }

        return potentialTargets.ToArray();
    }

    /// <summary>
    /// Sorter out targets to find the closest one
    /// </summary>
    private void AcquireTarget()
    {
        GameObject[] enemies = FindTargetInRange();
        EnemyFSM bestTarget = null;
        float closestDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (GameObject enemy in enemies)
        {
            Vector3 directionToTarget = enemy.transform.position - currentPosition;
            float magnitude = directionToTarget.magnitude; // Don't use sqrt as not necessary to compare distance

            if (magnitude < closestDistance)
            {
                closestDistance = magnitude;
                bestTarget = enemy.GetComponent<EnemyFSM>();
            }
        }

        // Once found, assign and follow it
        if(bestTarget != null)
        {
            cameraController.FollowTarget(bestTarget.transform);
            CharacterGameEvent.instance.TargetSelectedRaised(bestTarget.data);
            target = bestTarget;
        }
    }

    /// <summary>
    /// Release a known target
    /// </summary>
    private void ReleaseTarget()
    {
        if (target != null)
        {
            cameraController.ReleaseTarget(target.transform);
            CharacterGameEvent.instance.TargetDeselectedRaised(target.data);
            target = null;
        }
    }

    /// <summary>
    /// Rotate player toward a target or based on input
    /// </summary>
    public void Rotate()
    {
        if (target == null)
        {
            _playerRotation.y = Input.GetAxis("Horizontal");

            transform.Rotate(_playerRotation);
        }
        // Otherwise follow target
        else if (target != null)
        {
            transform.LookAt(target.transform);
        }
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
    /// Animation triggers a FX
    /// Called from animation event
    /// </summary>
    public void OnAnimationPlayFx()
    {
        PlayerStateAttack state = (PlayerStateAttack)currentState;

        if (state != null)
        {
            state.OnAttackPlayFx();
        }
    }

    /// <summary>
    /// Animation has ended
    /// Called from animation event
    /// </summary>
    public void OnSingleAnimationEnded()
    {
        if (data.isAlive == true)
        {
            TransitionToMove();
        }
    }
}