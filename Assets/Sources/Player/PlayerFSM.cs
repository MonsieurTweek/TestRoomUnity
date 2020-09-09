using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFSM : CharacterFSM, ICharacter
{
    private const uint CAN_ATTACK_MASK = (uint)CharacterStateEnum.MOVE;
    private const uint CAN_HIT_MASK = (uint)(CharacterStateEnum.MOVE | CharacterStateEnum.ATTACK);
    private const uint CAN_TARGET_MASK = (uint)~(CharacterStateEnum.IDLE | CharacterStateEnum.DIE);

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
    public float rotationSpeed = 1f;
    public float rangeForTarget = 15f;
    public GameObject fxPerkUnlock = null;

    // Transitions to states
    public override void TransitionToIdle() { ChangeState(stateIdle); }
    public void TransitionToMove() { ChangeState(stateMove); }
    public void TransitionToHit() { ChangeState(stateHit); }
    public void TransitionToDie() { ChangeState(stateDie); }
    public void TransitionToAttackLight() { if (CanAttack(CharacterStateAttack.AttackType.ALT_1)) ChangeState(stateAttack, (int)CharacterStateAttack.AttackType.ALT_1); }
    public void TransitionToAttackHeavy() { if (CanAttack(CharacterStateAttack.AttackType.ALT_2)) ChangeState(stateAttack, (int)CharacterStateAttack.AttackType.ALT_2); }

    public CharacterFSM target { private set; get; }
    public bool isGrounded { private set; get; }
    public bool isPaused { private set; get; }

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

        LoadingGameEvent.instance.onPlayerLoading += OnArchetypeLoaded;
    }

    private void OnArchetypeLoaded(PlayerData data)
    {
        this.data = data;

        // Save current player rotation to start with
        _playerRotation = transform.eulerAngles;

        // Bind player to events
        CharacterGameEvent.instance.onPause += OnPause;
        CharacterGameEvent.instance.onDied += OnDied;
        CharacterGameEvent.instance.onIntroStarted += OnIntroStarted;
        CharacterGameEvent.instance.onIntroEnded += OnIntroEnded;

        PerkGameEvent.instance.onUnlockStarted += OnPerkUnlockStarted;

        // Bind player common inputs
        InputManager.instance.gameplay.Target.started += OnTargetButtonStarted;
        InputManager.instance.gameplay.Target.canceled += OnTargetButtonCanceled;

        InputManager.instance.gameplay.AttackLight.canceled += ctx => TransitionToAttackLight();
        InputManager.instance.gameplay.AttackHeavy.canceled += ctx => TransitionToAttackHeavy();

        // Unbind archetype loading and complete the loading
        LoadingGameEvent.instance.onPlayerLoading -= OnArchetypeLoaded;
        LoadingGameEvent.instance.CompleteLoadingPlayer();

        TransitionToMove();
    }

    public override void Update()
    {
        base.Update();

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

#if UNITY_EDITOR
        Debug.DrawRay(model.position, new Vector3(0f, -rayLength, 0f), Color.yellow);

        if (target != null)
        {
            Vector3 direction = ((EnemyFSM)target).stateHit.anchor.position - stateAttack.anchor.position;

            Debug.DrawRay(stateAttack.anchor.position, direction, Color.red);
        }
#endif

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

    /// <summary>
    /// Check if a certain type attack is possible
    /// Otherwise catch that player tried to do it
    /// </summary>
    /// <param name="type">The type of attack to perform</param>
    /// <returns>Whether or not it's possible to do it now</returns>
    private bool CanAttack(CharacterStateAttack.AttackType type)
    {
        // Player can make an attack
        if (isGrounded == true && ((uint)currentState.flag & CAN_ATTACK_MASK) != 0)
        {
            float energyCost = type == CharacterStateAttack.AttackType.ALT_1 ? ((PlayerData)data).energyForLightAttack : ((PlayerData)data).energyForHeavyAttack;

            return ((PlayerData)data).ConsumeEnergy(energyCost);
        }
        // Player is already doing an attack
        else if (((uint)currentState.flag & (uint)CharacterStateEnum.ATTACK) != 0)
        {
            PlayerStateAttack state = ((PlayerStateAttack)currentState);

            // Player try to perform a different attack
            if (state.isComboAvailable == true && state.type == type)
            {
                // Keep track of it to chain the next attack
                state.TriggerCombo(type);
            }
        }

        return false;
    }

    /// <summary>
    /// Sorter out targets to find the closest one
    /// </summary>
    private void AcquireTarget()
    {
        if (((uint)currentState.flag & CAN_TARGET_MASK) != 0)
        {
            EnemyFSM[] enemies = FindTargetInRange();
            EnemyFSM bestTarget = null;
            float closestDistance = Mathf.Infinity;
            Vector3 currentPosition = transform.position;

            foreach (EnemyFSM enemy in enemies)
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
    }

    private EnemyFSM[] FindTargetInRange()
    {
        List<EnemyFSM> potentialTargets = new List<EnemyFSM>();
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, rangeForTarget);

        int i = 0;

        while (i < hitColliders.Length)
        {
            if (hitColliders[i].tag == EnemyData.TAG)
            {
                EnemyFSM enemy = hitColliders[i].GetComponent<EnemyFSM>();

                if (enemy.data.isAlive == true)
                {
                    potentialTargets.Add(enemy);
                }
            }
            i++;
        }

        return potentialTargets.ToArray();
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
            _playerRotation.y = InputManager.instance.gameplay.Rotate.ReadValue<float>();

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
        if (((uint)currentState.flag & CAN_HIT_MASK) != 0)
        {
            // Being in last combo step prevent any damage
            if (currentState.flag == (uint)CharacterStateEnum.ATTACK && ((PlayerStateAttack)currentState).isComboFinalStep == true)
            {
                return false;
            }

            data.ApplyDamage(damage);

            CharacterGameEvent.instance.Hit(data, damage);

            if (data.isAlive == true)
            {
                TransitionToHit();
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

    public void OnPause(bool isPauseEnabled)
    {
        isPaused = isPauseEnabled;

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

        if (data.uniqueId == uniqueId)
        {
            InputManager.instance.gameplay.Target.started -= OnTargetButtonStarted;
            InputManager.instance.gameplay.Target.canceled -= OnTargetButtonCanceled;
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

    private void OnPerkUnlockStarted(uint uniqueId, Perk perk)
    {
        perk.Unlock(this);

        if (perk.usage == Perk.PerkUsage.PERMANENT)
        {
            _perks.Add(perk);
        }

        Instantiate<GameObject>(fxPerkUnlock, transform);
    }

    private void OnTargetButtonStarted(InputAction.CallbackContext context)
    {
        AcquireTarget();
    }

    private void OnTargetButtonCanceled(InputAction.CallbackContext context)
    {
        ReleaseTarget();
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
    /// Animation event triggers a change of animation speed
    /// </summary>
    public void OnUpdateAttackSpeed(AnimationEvent animationEvent)
    {
        if (currentState.flag == (uint)CharacterStateEnum.ATTACK)
        {
            ((PlayerStateAttack)currentState).OnUpdateAttackSpeed(animationEvent.floatParameter);
        }
    }

    /// <summary>
    /// Animation event triggers a predifined FX to play based on the attack
    /// </summary>
    public void OnAttackPlayFx()
    {
        if (currentState.flag == (uint)CharacterStateEnum.ATTACK)
        {
            ((PlayerStateAttack)currentState).OnAttackPlayFx();
        }
    }

    public void OnToggleCombo()
    {
        if (currentState.flag == (uint)CharacterStateEnum.ATTACK)
        {
            ((PlayerStateAttack)currentState).ToggleCombo();
        }
    }

    /// <summary>
    /// Animation has ended
    /// Called from animation event
    /// </summary>
    public void OnSingleAnimationEnded()
    {
        if (isPaused == true)
        {
            return;
        }

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
        if (LoadingGameEvent.instance != null)
        {
            LoadingGameEvent.instance.onPlayerLoading -= OnArchetypeLoaded;
        }

        if (CharacterGameEvent.instance != null)
        {
            CharacterGameEvent.instance.onPause -= OnPause;
            CharacterGameEvent.instance.onIntroStarted -= OnIntroStarted;
            CharacterGameEvent.instance.onIntroEnded -= OnIntroEnded;
        }

        if (PerkGameEvent.instance != null)
        {
            PerkGameEvent.instance.onUnlockStarted -= OnPerkUnlockStarted;
        }

        if (InputManager.instance != null)
        {
            InputManager.instance.gameplay.Target.started -= OnTargetButtonStarted;
            InputManager.instance.gameplay.Target.canceled -= OnTargetButtonCanceled;
        }
    }
}