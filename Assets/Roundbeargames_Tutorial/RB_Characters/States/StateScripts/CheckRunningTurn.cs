using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames_tutorial
{
    [CreateAssetMenu(fileName = "CheckRunningTurn", menuName = "Roundbeargames/AbilityData/CheckRunningTurn")]
    public class CheckRunningTurn : StateData
    {
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);

            if (control.IsFaceingForward())
            {
                animator.SetBool(TransitionParameter.Turn.ToString(), control.moveLeft);
            }
            else
            {
                animator.SetBool(TransitionParameter.Turn.ToString(), control.moveRight);
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool(TransitionParameter.Turn.ToString(), false);
        }
    }
}
