﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "TriggerRagdoll", menuName = "Roundbeargames/Death/TriggerRagdoll")]
    public class TriggerRagdoll : StateData
    {
        public float triggerTiming;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (stateInfo.normalizedTime > triggerTiming)
            {
                if (!characterState.characterControl.animationProgress.ragdollTriggered)
                {
                    if (characterState.characterControl.skinedMeshAnimator.enabled)
                    {
                        characterState.characterControl.animationProgress.ragdollTriggered = true;
                    }
                }
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            //CharacterControl control = characterState.GetCharacterControl(animator);
            //control.animationProgress.ragdollTriggered = false;
        }
    }
}
