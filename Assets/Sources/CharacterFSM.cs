using UnityEngine;

/// <summary>
/// Define a simple Final State Machine to manage states
/// </summary>
public class CharacterFSM : MonoBehaviour
{
    //Base class for all states, only the required methods need to be overriden 
    public class StateBase
    {
        [HideInInspector] public CharacterFSM owner = null;
        [HideInInspector] public StateEnum flag = StateEnum.NONE;

        public virtual void Update() { }
        public virtual void FixedUpdate() { }
        public virtual void Exit() { }

        public override string ToString()
        {
            return base.ToString() + " with flag " + flag;
        }
    };

    public class State : StateBase { public virtual void Enter() { } }
    public abstract class State1Param<T> : StateBase { public abstract void Enter(T p); }
    public abstract class State2Params<T0, T1> : StateBase { public abstract void Enter(T0 p0, T1 p1); }
    public abstract class State3Params<T0, T1, T2> : StateBase { public abstract void Enter(T0 p0, T1 p1, T2 p2); }

    //Current state being handled in this FSM
    public StateBase currentState { private set; get; }

    private bool ChangeStateBase(StateBase newState)
    {
        //Exit the current state
        if (currentState != null)
        {
            currentState.Exit();
        }

        //Change to the new state
        currentState = newState;

        Debug.Log(currentState);

        if (newState != null)
        {
            newState.owner = this;
            return true;
        }

        return false;
    }

    public void ChangeState(State newState)
    {
        if (ChangeStateBase(newState))
        {
            newState.Enter();
        }
    }

    public void ChangeState<T>(State1Param<T> newState, T p)
    {
        if (ChangeStateBase(newState))
        {
            newState.Enter(p);
        }
    }

    public void ChangeState<T0, T1>(State2Params<T0, T1> newState, T0 p0, T1 p1)
    {
        if (ChangeStateBase(newState))
        {
            newState.Enter(p0, p1);
        }
    }

    public void ChangeState<T0, T1, T2>(State3Params<T0, T1, T2> newState, T0 p0, T1 p1, T2 p2)
    {
        if (ChangeStateBase(newState))
        {
            newState.Enter(p0, p1, p2);
        }
    }

    public virtual void Update()
    {
        //Update the currentState
        if (currentState != null)
        {
            currentState.Update();
        }
    }

    public virtual void FixedUpdate()
    {
        //Update the currentState
        if (currentState != null)
        {
            currentState.FixedUpdate();
        }
    }
}