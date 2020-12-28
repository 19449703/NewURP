using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace roundbeargames_tutorial
{
    public enum AITransitionType
    {
        RUN_TO_WALK,
        WALK_TO_RUN,
    }

    [CreateAssetMenu(fileName = "AITransitionCondition", menuName = "Roundbeargames/AI/AITransitionCondition")]
    public class AITransitionCondition : StateData
    {
        public AITransitionType aiTransition;
        public AI_Type nextAI;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);
            if (TransitionToNextAI(control))
            {
                control.aiController.TriggerAI(nextAI);
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        bool TransitionToNextAI(CharacterControl control)
        {
            if (aiTransition == AITransitionType.RUN_TO_WALK)
            {
                Vector3 dir = control.aiProgress.pathFindingAgent.startSphere.transform.position - control.transform.position;
                if (Vector3.SqrMagnitude(dir) < 2)
                {
                    return true;
                }
            }
            else if (aiTransition == AITransitionType.WALK_TO_RUN)
            {
                Vector3 dir = control.aiProgress.pathFindingAgent.startSphere.transform.position - control.transform.position;
                if (Vector3.SqrMagnitude(dir) > 2)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
