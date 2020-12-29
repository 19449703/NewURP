using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames_tutorial
{
    [CreateAssetMenu(fileName = "Jump", menuName = "Roundbeargames/AbilityData/Jump")]
    public class Jump : StateData
    {
        [Range(0f, 1f)]
        public float jumpTiming;
        public float jumpForce;
        public AnimationCurve pull;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);

            if (jumpTiming == 0f)
            {
                control.RIGID_BODY.AddForce(Vector3.up * jumpForce);
                control.animationProgress.jumped = true;
            }

            animator.SetBool(TransitionParameter.Grounded.ToString(), false);
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);

            control.pullMultiplier = pull.Evaluate(stateInfo.normalizedTime);

            if (!control.animationProgress.jumped && stateInfo.normalizedTime >= jumpTiming)
            {
                control.RIGID_BODY.AddForce(Vector3.up * jumpForce);
                control.animationProgress.jumped = true;
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);
            control.pullMultiplier = 0;
            //control.animationProgress.jumped = false;
        }
    }
}