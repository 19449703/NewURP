using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "Idle", menuName = "Roundbeargames/AbilityData/Idle")]
    public class Idle : StateData
    {
		public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
		{
            animator.SetBool(TransitionParameter.Jump.ToString(), false);
            animator.SetBool(TransitionParameter.Attack.ToString(), false);
            animator.SetBool(TransitionParameter.Move.ToString(), false);

            CharacterControl control = characterState.characterControl;
            control.animationProgress.disallowEarlyTurn = false;
        }

		public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
			CharacterControl control = characterState.characterControl;

            if (control.attack)
            {
                animator.SetBool(TransitionParameter.Attack.ToString(), true);
            }

            if (control.jump)
			{
                if (!control.animationProgress.jumped)
                {
                    animator.SetBool(TransitionParameter.Jump.ToString(), true);
                }
            }
            else
            {
                control.animationProgress.jumped = false;
            }

            if (control.moveLeft && control.moveRight)
            {
                // do nothing
            }
            else if (control.moveLeft)
            {
                animator.SetBool(TransitionParameter.Move.ToString(), true);
            }
            if (control.moveRight)
            {
                animator.SetBool(TransitionParameter.Move.ToString(), true);
            }
        }

		public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
		{
		}
	}
}
