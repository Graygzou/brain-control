using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public GameObject particles;            // Prefab of the particle system to play in the state.
    public AvatarIKGoal attackLimb;         // The limb that the particles should follow.

    private Transform particlesTransform;       // Reference to the instantiated prefab's transform.
    private ParticleSystem particleSystem;      // Reference to the instantiated prefab's particle system.

    private AnimatedTexture animTextScript;
    public int count; // Nombre d'image affichées
    private float cooldown = 0.7f;

    // This will be called when the animator first transitions to this state.
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // If the particle system already exists then exit the function.
        if (particlesTransform != null)
            return;

        // Otherwise instantiate the particles and set up references to their components.
        GameObject particlesInstance = Instantiate(particles);
        particlesTransform = particlesInstance.transform;
        particleSystem = particlesInstance.GetComponent<ParticleSystem>();
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



    // OnStateMove is called before OnAnimatorMove would be called in MonoBehaviours for every frame the state is playing.
    // When OnStateMove is called, it will stop OnAnimatorMove from being called in MonoBehaviours.
    override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animTextScript = (AnimatedTexture)animator.gameObject.GetComponentInChildren(typeof(AnimatedTexture));

        if (animTextScript != null)
        {
            animTextScript.setCounter(count);
            animTextScript.LancerAnimation(/*cooldown*/);
        }
    }


    // This will be called once the animator has transitioned out of the state.
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // When leaving the special move state, stop the particles.
        particleSystem.Stop();
    }


    // OnStateIK is called after OnAnimatorIK on MonoBehaviours for every frame the while the state is being played.
    // It is important to note that OnStateIK will only be called if the state is on a layer that has an IK pass. 
    // By default, layers do not have an IK pass and so this function will not be called.
    // For more information on IK see the information linked below.
    override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // OnStateExit may be called before the last OnStateIK so we need to check the particles haven't been destroyed.
        if (particleSystem == null || particlesTransform == null)
            return;

        // Find the position and rotation of the limb the particles should follow.
        Vector3 limbPosition = animator.GetIKPosition(attackLimb);
        Quaternion limbRotation = animator.GetIKRotation(attackLimb);

        // Set the particle's position and rotation based on that limb.
        particlesTransform.position = limbPosition;
        particlesTransform.rotation = limbRotation;

        // If the particle system isn't playing, play it.
        if (!particleSystem.isPlaying)
            particleSystem.Play();
    }
}
