using System.Collections.Generic;
using UnityEngine;

public class PlayerFSM : CharacterFSM, ICharacter
{
    private const uint FLAG_CAN_ATTACK = (uint)StateEnum.MOVE;
    private const uint FLAG_CAN_HIT = (uint)(StateEnum.MOVE | StateEnum.ATTACK);

    [Header("References")]
    public Transform model = null;
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
    public bool isGrounded { private set; get; }

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

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        RaycastHit hit;
        float rayLength = 10.0f; //use any value.
                                    //shoot a debug ray down to show you the ray it's casting--have to turn gizmos on in gameView window

        Debug.DrawRay(model.position, new Vector3(0f, -rayLength, 0f), Color.yellow);

        //cast the actual ray for ground detection
        if (Physics.Raycast(model.position, model.TransformDirection(Vector3.down), out hit, rayLength) == true)
        {
            //if the RayCast hit something below the player
            //check the actual distance of the hit object
            isGrounded = hit.distance <= 0.15f;
        }
        // if nothing found assume player is grounded
        else
        {
            isGrounded = true;
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
            Vector3 direction = target.transform.position - transform.position;
            direction.y = 0.0f;

            // Rotate over time to get a smooth look at
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed);
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