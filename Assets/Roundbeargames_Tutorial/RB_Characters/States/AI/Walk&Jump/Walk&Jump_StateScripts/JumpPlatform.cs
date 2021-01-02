using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "JumpPlatform", menuName = "Roundbeargames/AI/JumpPlatform")]
    public class JumpPlatform : StateData
    {
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);
            control.jump = true;
            control.moveUp = true;

            if (control.aiProgress.pathFindingAgent.startSphere.transform.position.z <
                    control.aiProgress.pathFindingAgent.endSphere.transform.position.z)
            {
                control.FaceForward(true);
            }
            else
            {
                control.FaceForward(false);
            }
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);

            float topDist = control.aiProgress.pathFindingAgent.endSphere.transform.position.y -
                control.frontSpheres[1].transform.position.y;
            float bottomDist = control.aiProgress.pathFindingAgent.endSphere.transform.position.y -
                control.frontSpheres[0].transform.position.y;

            if (topDist < 1.5f && bottomDist > 0.5f)
            {
                control.moveLeft = !control.IsFaceingForward();
                control.moveRight = control.IsFaceingForward();
            }

            if (bottomDist < 0.5f)
            {
                control.moveLeft = false;
                control.moveRight = false;
                control.moveUp = true;
                control.jump = false;

                animator.gameObject.SetActive(false);
                animator.gameObject.SetActive(true);
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            
        }
    }
}
