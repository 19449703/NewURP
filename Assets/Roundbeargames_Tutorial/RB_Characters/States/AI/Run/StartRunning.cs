using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "StartRunning", menuName = "Roundbeargames/AI/StartRunning")]
    public class StartRunning : StateData
    {
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.characterControl;

            Vector3 dir = control.aiProgress.pathFindingAgent.startSphere.transform.position - control.transform.position;
            control.FaceForward(dir.z > 0);
            control.moveLeft = dir.z < 0;
            control.moveRight = dir.z > 0;

            if (control.aiProgress.GetDistanceToDestination() > 2f)
            {
                control.turbo = true;
            }

            control.moveUp = false;
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.characterControl;

            if (control.aiProgress.GetDistanceToDestination() < 2f)
            {
                control.moveLeft = false;
                control.moveRight = false;
                control.turbo = false;
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            
        }
    }
}
