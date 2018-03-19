using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{

    // This will be called when the animator first transitions to this state.
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    //OnStateUpdate is called after MonoBehaviour Updates on every frame whilst the animator is playing the state this behaviour belongs to.
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
    }

    //OnStateMachineEnter is called on the first frame that the animator plays the contents of a Sub-State Machine.
    public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        base.OnStateMachineEnter(animator, stateMachinePathHash);
    }

    //OnStateMachineExit is called on the last frame of a transition from a Sub-State Machine.
    public override void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        base.OnStateMachineExit(animator, stateMachinePathHash);
    }
}
