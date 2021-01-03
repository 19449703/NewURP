using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "Jump", menuName = "Roundbeargames/AbilityData/Jump")]
    public class Jump : StateData
    {
        [Range(0f, 1f)]
        public float jumpTiming;
        public float jumpForce;
        [Header("Extra Gravity")]
        public AnimationCurve pull;
        public bool cancelPull;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.characterControl;
            
            control.animationProgress.jumped = false;

            if (jumpTiming == 0f)
            {
                control.RIGID_BODY.AddForce(Vector3.up * jumpForce);
                control.animationProgress.jumped = true;
            }

            control.animationProgress.cancelPull = cancelPull;

            animator.SetBool(TransitionParameter.Grounded.ToString(), false);
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.characterControl;

            control.pullMultiplier = pull.Evaluate(stateInfo.normalizedTime);

            if (!control.animationProgress.jumped && stateInfo.normalizedTime >= jumpTiming)
            {
                control.RIGID_BODY.AddForce(Vector3.up * jumpForce);
                control.animationProgress.jumped = true;
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.characterControl;
            control.pullMultiplier = 0;
            //control.animationProgress.jumped = false;
        }
    }
}