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
        //public AnimationCurve gravity;
        public AnimationCurve pull;
        private bool isJumped;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);

            if (jumpTiming == 0f)
            {
                control.RIGID_BODY.AddForce(Vector3.up * jumpForce);
                isJumped = true;
            }

            animator.SetBool(TransitionParameter.Grounded.ToString(), false);
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);

            //control.gravityMultiplier = gravity.Evaluate(stateInfo.normalizedTime);
            control.pullMultiplier = pull.Evaluate(stateInfo.normalizedTime);

            if (!isJumped && stateInfo.normalizedTime >= jumpTiming)
            {
                control.RIGID_BODY.AddForce(Vector3.up * jumpForce);
                isJumped = true;
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);
            control.pullMultiplier = 0;
            isJumped = false;
        }
    }
}
