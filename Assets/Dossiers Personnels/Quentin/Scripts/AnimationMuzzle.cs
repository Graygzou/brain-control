using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationMuzzle : StateMachineBehaviour
{

    private AnimatedTexture animTextScript;
    //public int count; // Nombre d'image affichées
    //public float cooldown = 0.7f;

    // This will be called when the animator first transitions to this state.
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animTextScript = (AnimatedTexture)animator.gameObject.GetComponentInChildren(typeof(AnimatedTexture));

        if (animTextScript != null)
        {
            animTextScript.LancerAnimation(/*count,cooldown*/);
        }
    }


    // This will be called once the animator has transitioned out of the state.
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animTextScript = (AnimatedTexture)animator.gameObject.GetComponentInChildren(typeof(AnimatedTexture));

        if (animTextScript != null)
        {
            animTextScript.Cancel();
        }
    }


    // This will be called every frame whilst in the state.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //}
}
