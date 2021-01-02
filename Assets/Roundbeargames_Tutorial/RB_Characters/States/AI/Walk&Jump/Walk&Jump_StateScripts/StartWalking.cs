using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "StartWalking", menuName = "Roundbeargames/AI/StartWalking")]
    public class StartWalking : StateData
    {
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);

            Vector3 dir = control.aiProgress.pathFindingAgent.startSphere.transform.position - control.transform.position;
            control.moveLeft = dir.z < 0;
            control.moveRight = dir.z > 0;
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);
            Vector3 dir = control.aiProgress.pathFindingAgent.startSphere.transform.position - control.transform.position;

            // jump
            if (control.aiProgress.pathFindingAgent.startSphere.transform.position.y <
                    control.aiProgress.pathFindingAgent.endSphere.transform.position.y)
            {
                if (Vector3.SqrMagnitude(dir) < 0.01f)
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

            // straight
            if (control.aiProgress.pathFindingAgent.startSphere.transform.position.y ==
                    control.aiProgress.pathFindingAgent.endSphere.transform.position.y)
            {
                if (Vector3.SqrMagnitude(dir) < 0.5f)
                {
                    control.moveLeft = false;
                    control.moveRight = false;

                    Vector3 playerDist = control.transform.position - CharacterManager.instance.GetPlayableCharacter().transform.position;
                    if (playerDist.sqrMagnitude > 1)
                    {
                        animator.gameObject.SetActive(false);
                        animator.gameObject.SetActive(true);
                    }
                    // 临时攻击方案
                    //else
                    //{
                    //    if (CharacterManager.instance.GetPlayableCharacter().damageDetector.damageTaken == 0)
                    //    {
                    //        if (control.IsFaceingForward())
                    //        {
                    //            control.moveLeft = false;
                    //            control.moveRight = true;
                    //            control.attack = true;
                    //        }
                    //        else
                    //        {
                    //            control.moveLeft = true;
                    //            control.moveRight = false;
                    //            control.attack = true;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        control.attack = false;
                    //    }
                    //}
                }
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool(AI_Walk_Transitions.jump_platform.ToString(), false);
            animator.SetBool(AI_Walk_Transitions.fall_platform.ToString(), false);
        }
    }
}
