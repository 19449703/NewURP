using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames_tutorial
{
    [CreateAssetMenu(fileName = "UpdateBoxCollider", menuName = "Roundbeargames/AbilityData/UpdateBoxCollider")]
    public class UpdateBoxCollider : StateData
    {
        public Vector3 targetSize;
        public float sizeUpdateSpeed;

        public Vector3 targetCenter;
        public float centerUpdateSpeed;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);

            control.animationProgress.updatingBoxCollider = true;
            control.animationProgress.targetSize = targetSize;
            control.animationProgress.sizeSpeed = sizeUpdateSpeed;
            control.animationProgress.targetCenter = targetCenter;
            control.animationProgress.centerSpeed = centerUpdateSpeed;
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
        }
    }
}
