using System;
using UnityEngine;

/// <summary>
/// Define a simple Final State Machine to manage character states
/// </summary>
public class CharacterFSM : AbstractFSM
{
    // Common references across FSMs
    public Animator animator = null;

    //Base class for all states, only the required methods need to be overriden 
    public class CharacterStateBase : StateBase
    {
        [HideInInspector] public CharacterFSM character = null;

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