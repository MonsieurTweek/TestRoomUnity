using UnityEngine;

/// <summary>
/// Define a simple Final State Machine to manage character states
/// </summary>
public class CharacterFSM : AbstractFSM
{
    /// List of events used in animation clips
    public static string ANIM_EVENT_MANUAL              = "OnManualAnimationEvent";
    public static string ANIM_EVENT_ENDED               = "OnSingleAnimationEnded";
    public static string ANIM_EVENT_PLAY_FX             = "OnAnimationPlayFx";
    public static string ANIM_EVENT_SEND_PROJECTILE     = "OnAnimationSendProjectile";
    public static string ANIM_EVENT_SPAWN_MINION        = "OnAnimationSpawnMinion";
    public static string ANIM_EVENT_UPDATE_SPEED        = "OnUpdateAttackSpeed";
    public static string ANIM_EVENT_TOGGLE_ATTACK       = "OnToggleAttack";
    public static string ANIM_EVENT_TRIGGER_HEAL        = "OnTriggerHeal";

    // Common references across FSMs
    public Animator animator = null;
    public CharacterFxController fxController = null;
    public CharacterGearController gearController = null;

    //Base class for all states, only the required methods need to be overriden 
    public class CharacterStateBase : StateBase
    {
        [HideInInspector] public CharacterFSM character = null;

        public virtual void OnSingleAnimationEnded()
        {
            character.TransitionToIdle();
        }

        public override string ToString()
        {
            return base.ToString() + " with flag " + (CharacterStateEnum)flag;
        }
    };

    public class CharacterState : CharacterStateBase { public virtual void Enter() { } }
    public abstract class CharacterState1Param<T> : CharacterStateBase { public abstract void Enter(T p); }
    public abstract class CharacterState2Params<T0, T1> : CharacterStateBase { public abstract void Enter(T0 p0, T1 p1); }
    public abstract class CharacterStateState3Params<T0, T1, T2> : CharacterStateBase { public abstract void Enter(T0 p0, T1 p1, T2 p2); }

    public AbstractCharacterData data { protected set; get; }

    // Common states for characters
    public CharacterStateStun stateStun = new CharacterStateStun();

    // Transitions to states
    public virtual void TransitionToIdle() { }
    public void TransitionToStun(float duration) { ChangeState(stateStun, duration, TransitionToIdle); }

    protected virtual void Awake()
    {
        stateStun.flag = (uint)CharacterStateEnum.STUN;
    }

    public override void AttachOwner()
    {
        base.AttachOwner();

        ((CharacterStateBase)currentState).character = this;
    }

    public void ChangeState(CharacterState newState)
    {
        if (ChangeStateBase(newState))
        {
            newState.Enter();
        }
    }

    public void ChangeState<T>(CharacterState1Param<T> newState, T p)
    {
        if (ChangeStateBase(newState))
        {
            newState.Enter(p);
        }
    }

    public void ChangeState<T0, T1>(CharacterState2Params<T0, T1> newState, T0 p0, T1 p1)
    {
        if (ChangeStateBase(newState))
        {
            newState.Enter(p0, p1);
        }
    }

    public void ChangeState<T0, T1, T2>(CharacterStateState3Params<T0, T1, T2> newState, T0 p0, T1 p1, T2 p2)
    {
        if (ChangeStateBase(newState))
        {
            newState.Enter(p0, p1, p2);
        }
    }

    /// <summary>
    /// Empty function receiver for animation event handle manually
    /// </summary>
    public void OnManualAnimationEvent() {}

    /// <summary>
    /// Animation enable/disable attack on correct moment
    /// </summary>
    public void OnToggleAttack()
    {
        if (currentState.flag == (uint)CharacterStateEnum.ATTACK)
        {
            ((CharacterStateAttack)currentState).OnToggleAttack();
        }
    }

    /// <summary>
    /// Animation triggers a FX
    /// Called from animation event
    /// </summary>
    public void OnAnimationPlayFx(AnimationEvent animationEvent)
    {
        fxController.PlayFx(animationEvent.objectReferenceParameter, animationEvent.intParameter == 1, currentState.flag);
    }

    /// <summary>
    /// Animation triggers a SFX
    /// Called from animation event
    /// </summary>
    public void OnAnimationPlaySfx(AnimationEvent animationEvent)
    {
        AudioManager.instance.PlayInGameSound((AudioClip)animationEvent.objectReferenceParameter);
    }

    /// <summary>
    /// Animation triggers the gear visual effect
    /// </summary>
    /// <param name="animationEvent">intParameter : 0:right, 1:left, 2:both</param>
    public void OnPlayGearFx(AnimationEvent animationEvent)
    {
        gearController.PlayFx(animationEvent.intParameter);
    }

#if UNITY_EDITOR
    public virtual void OnDrawGizmos()
    {
        if (currentState != null)
        {
            UnityEditor.Handles.Label(transform.position + Vector3.up, "State : " + currentState);

            currentState.OnDrawGizmos();
        }
    }
#endif
}