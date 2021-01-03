using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "FallPlatform", menuName = "Roundbeargames/AI/FallPlatform")]
    public class FallPlatform : StateData
    {
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.characterControl;

            if (control.transform.position.z <
                    control.aiProgress.pathFindingAgent.endSphere.transform.position.z)
            {
                control.FaceForward(true);
            }
            else if (control.transform.position.z >
                    control.aiProgress.pathFindingAgent.endSphere.transform.position.z)
            {
                control.FaceForward(false);
            }
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.characterControl;
            if (control.IsFaceingForward())
            {
                if (control.transform.position.z <
                    control.aiProgress.pathFindingAgent.endSphere.transform.position.z)
                {
                    control.moveRight = true;
                    control.moveLeft = false;
                }
                else
                {
                    control.moveRight = false;
                    control.moveLeft = false;

                    animator.gameObject.SetActive(false);
                    animator.gameObject.SetActive(true);
                }
            }
            else
            {
                if (control.transform.position.z >
                    control.aiProgress.pathFindingAgent.endSphere.transform.position.z)
                {
                    control.moveRight = false;
                    control.moveLeft = true;
                }
                else
                {
                    control.moveRight = false;
                    control.moveLeft = false;

                    animator.gameObject.SetActive(false);
                    animator.gameObject.SetActive(true);
                }
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}
