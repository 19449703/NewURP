using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "CheckRunningTurn", menuName = "Roundbeargames/AbilityData/CheckRunningTurn")]
    public class CheckRunningTurn : StateData
    {
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.characterControl;

            if (control.IsFaceingForward())
            {
                if (control.moveLeft)
                {
                    animator.SetBool(TransitionParameter.Turn.ToString(), true);
                }
                //animator.SetBool(TransitionParameter.Turn.ToString(), control.moveLeft);
            }
            else
            {
                if (control.moveRight)
                {
                    animator.SetBool(TransitionParameter.Turn.ToString(), true);
                }
                //animator.SetBool(TransitionParameter.Turn.ToString(), control.moveRight);
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool(TransitionParameter.Turn.ToString(), false);
        }
    }
}
