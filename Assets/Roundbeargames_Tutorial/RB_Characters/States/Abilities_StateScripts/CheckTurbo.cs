using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames_tutorial
{
    [CreateAssetMenu(fileName = "CheckTurbo", menuName = "Roundbeargames/AbilityData/CheckTurbo")]
    public class CheckTurbo : StateData
    {
        public bool museRequireMovement;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);

            if (control.turbo)
            {
                if (museRequireMovement)
                {
                    if (control.moveLeft || control.moveRight)
                    {
                        animator.SetBool(TransitionParameter.Turbo.ToString(), true);
                    }
                    else
                    {
                        animator.SetBool(TransitionParameter.Turbo.ToString(), false);
                    }
                }
                else
                {
                    animator.SetBool(TransitionParameter.Turbo.ToString(), true);
                }
            }
            else
            {
                animator.SetBool(TransitionParameter.Turbo.ToString(), false);
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
        }
    }
}
