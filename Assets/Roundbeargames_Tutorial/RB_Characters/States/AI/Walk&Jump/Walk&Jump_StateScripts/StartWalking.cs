using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "StartWalking", menuName = "Roundbeargames/AI/StartWalking")]
    public class StartWalking : StateData
    {
        public Vector3 targetDir = new Vector3();

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            WalkStraightTowardsTarget(characterState.characterControl);
        }

        public void WalkStraightTowardsTarget(CharacterControl control)
        {
            targetDir = control.aiProgress.pathFindingAgent.startSphere.transform.position - control.transform.position;
            control.moveLeft = targetDir.z < 0;
            control.moveRight = targetDir.z > 0;
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.characterControl;
            Vector3 dir = control.aiProgress.pathFindingAgent.startSphere.transform.position - control.transform.position;

            // jump
            if (control.aiProgress.pathFindingAgent.startSphere.transform.position.y <
                    control.aiProgress.pathFindingAgent.endSphere.transform.position.y)
            {
                if (control.aiProgress.GetDistanceToDestination() < 0.01f)
                {
                    control.moveLeft = false;
                    control.moveRight = false;

                    animator.SetBool(AI_Walk_Transitions.jump_platform.ToString(), true);
                }
            }

            // fall
            if (control.aiProgress.pathFindingAgent.startSphere.transform.position.y >
                    control.aiProgress.pathFindingAgent.endSphere.transform.position.y)
            {
                animator.SetBool(AI_Walk_Transitions.fall_platform.ToString(), true);
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool(AI_Walk_Transitions.jump_platform.ToString(), false);
            animator.SetBool(AI_Walk_Transitions.fall_platform.ToString(), false);
        }
    }
}
