﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "UpdateBoxCollider", menuName = "Roundbeargames/AbilityData/UpdateBoxCollider")]
    public class UpdateBoxCollider : StateData
    {
        public Vector3 targetCenter;
        public float centerUpdateSpeed;

        [Space(10)]
        public Vector3 targetSize;
        public float sizeUpdateSpeed;
        [Space(10)]
        public bool keepUpdating;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.characterControl;

            control.animationProgress.updatingBoxCollider = true;
            control.animationProgress.targetSize = targetSize;
            control.animationProgress.sizeSpeed = sizeUpdateSpeed;
            control.animationProgress.targetCenter = targetCenter;
            control.animationProgress.centerSpeed = centerUpdateSpeed;
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.characterControl;

            if (!keepUpdating)
            {
                control.animationProgress.updatingBoxCollider = false;
            }
        }
    }
}
