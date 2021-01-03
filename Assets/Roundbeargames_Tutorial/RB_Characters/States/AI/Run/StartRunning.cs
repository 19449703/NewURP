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

            Vector3 dist = control.aiProgress.pathFindingAgent.startSphere.transform.position - control.transform.position;
            if (Vector3.SqrMagnitude(dist) > 2f)
            {
                control.turbo = true;
            }
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.characterControl;
            Vector3 dist = control.aiProgress.pathFindingAgent.startSphere.transform.position - control.transform.position;

            if (Vector3.SqrMagnitude(dist) < 2f)
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
