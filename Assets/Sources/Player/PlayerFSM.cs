using System.Collections.Generic;
using UnityEngine;

public class PlayerFSM : CharacterFSM, ICharacter
{
    private const uint FLAG_CAN_ATTACK = (uint)CharacterStateEnum.MOVE;
    private const uint FLAG_CAN_HIT = (uint)(CharacterStateEnum.MOVE | CharacterStateEnum.ATTACK);

    [Header("References")]
    public Transform model = null;
    public PlayerCameraController cameraController = null;

    [Header("States")]
    public PlayerStateIdle stateIdle = new PlayerStateIdle();
    public PlayerStateMove stateMove = new PlayerStateMove();
    public PlayerStateAttack stateAttack = new PlayerStateAttack();
    public PlayerStateHit stateHit = new PlayerStateHit();
    public PlayerStateDie stateDie = new PlayerStateDie();

    [Header("Properties")]
    public Character configuration = null;
    public float rotationSpeed = 1f;
    public float rangeForTarget = 15f;
    public GameObject fxPerkUnlock = null;

    // Transitions to states
    public override void TransitionToIdle() { ChangeState(stateIdle); }
    public void TransitionToMove() { ChangeState(stateMove); }
    public void TransitionToHit() { ChangeState(stateHit); }
    public void TransitionToDie() { ChangeState(stateDie); }
    public void TransitionToAttack(bool isHeavy) { ChangeState(stateAttack, isHeavy); }

    public CharacterFSM target { private set; get; }
    public bool isGrounded { private set; get; }

    private Vector2 _playerRotation = Vector2.zero;
    private List<Perk> _perks = new List<Perk>();

    protected override void Awake()
    {
        base.Awake();

        stateIdle.flag = (uint)CharacterStateEnum.IDLE;
        stateMove.flag = (uint)CharacterStateEnum.MOVE;
        stateAttack.flag = (uint)CharacterStateEnum.ATTACK;
        stateHit.flag = (uint)CharacterStateEnum.HIT;
        stateDie.flag = (uint)CharacterStateEnum.DIE;

        data = new PlayerData();

        if (configuration != null)
        {
            data.Populate(configuration);

            // Save current player rotation to start with
            _playerRotation = transform.eulerAngles;

            // Bind player to pause
            CharacterGameEvent.instance.onPause += OnPause;
            CharacterGameEvent.instance.onDied += OnDied;
            CharacterGameEvent.instance.onIntroStarted += OnIntroStarted;
            CharacterGameEvent.instance.onIntroEnded += OnIntroEnded;

            PerkGameEvent.instance.onUnlocked += OnPerkUnlocked;
        }
        else
        {
            Debug.LogError("Missing character configuration file for player " + name + " initialization");

            TransitionToDie();
        }
    }

    private void Start()
    {
        TransitionToMove();
    }

    public override void Update()
    {
        base.Update();

        if (isGrounded == true && ((uint)currentState.flag & FLAG_CAN_ATTACK) != 0)
        {
            // Light Attack
            if (Input.GetMouseButtonUp(0) == true)
            {
                TransitionToAttack(false);
            }
            // Heavy Attack
            else if (Input.GetMouseButtonUp(1) == true)
            {
                TransitionToAttack(true);
            }
        }

        // Toggle target mode
        if (Input.GetKeyUp(KeyCode.Tab) == true)
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

        // Evaluate all perks in case it has something to update
        for (int i = 0; i < _perks.Count; i++)
        {
            _perks[i].Evaluate(this);
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
            CharacterGameEvent.instance.SelectTarget(bestTarget.data);
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
            CharacterGameEvent.instance.DeselectTarget(target.data);
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
                TransitionToHit();
            }
            else
            {
                TransitionToDie();
            }

            return true;
        }

        return false;
    }

    public void OnPause(bool isPauseEnabled)
    {
        if (isPauseEnabled == true)
        {
            TransitionToIdle();
        }
        else
        {
            TransitionToMove();
        }
    }

    private void OnDied(uint uniqueId, int _)
    {
        if (target != null && target.data.uniqueId == uniqueId)
        {
            ReleaseTarget();
        }
    }

    private void OnIntroStarted(Transform _, AbstractCharacterData __)
    {
        OnPause(true);

        ReleaseTarget();
    }

    private void OnIntroEnded()
    {
        OnPause(false);
    }

    private void OnPerkUnlocked(uint uniqueId, Perk perk)
    {
        perk.Unlock(this);

        if (perk.type == Perk.PerkType.PERMANENT)
        {
            _perks.Add(perk);
        }

        Instantiate<GameObject>(fxPerkUnlock, transform);
    }

    /// <summary>
    /// Animation triggers a FX
    /// Called from animation event
    /// </summary>
    public void OnAnimationPlayFx(AnimationEvent animationEvent)
    {
        if (currentState.flag == (uint)CharacterStateEnum.ATTACK)
        {
            ((PlayerStateAttack)currentState).OnAttackPlayFx(animationEvent.objectReferenceParameter, animationEvent.intParameter == 1);
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
            ((PlayerStateAttack)currentState).OnAttackSendProjectile(animationEvent.objectReferenceParameter, animationEvent.intParameter == 1);
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
        else
        {
            currentState.Exit();
        }
    }

    private void OnDestroy()
    {
        if (CharacterGameEvent.instance != null)
        {
            CharacterGameEvent.instance.onPause -= OnPause;
            CharacterGameEvent.instance.onIntroStarted -= OnIntroStarted;
            CharacterGameEvent.instance.onIntroEnded -= OnIntroEnded;
        }

        if (PerkGameEvent.instance != null)
        {
            PerkGameEvent.instance.onUnlocked -= OnPerkUnlocked;
        }
    }
}