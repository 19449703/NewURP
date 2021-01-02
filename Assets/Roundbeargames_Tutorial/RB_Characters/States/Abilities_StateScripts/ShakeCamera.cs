using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "ShakeCamera", menuName = "Roundbeargames/AbilityData/ShakeCamera")]
    public class ShakeCamera : StateData
    {
        [Range(0f, 0.99f)]
        public float shakeTiming;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (shakeTiming == 0f)
            {
                CharacterControl control = characterState.GetCharacterControl(animator);
                CameraManager.instance.ShakeCamera(0.2f);
                control.animationProgress.cameraShaken = true;
            }
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);
            if (!control.animationProgress.cameraShaken)
            {
                if (stateInfo.normalizedTime >= shakeTiming)
                {
                    control.animationProgress.cameraShaken = true;
                    CameraManager.instance.ShakeCamera(0.2f);
                }
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);
            control.animationProgress.cameraShaken = false;
        }
    }
}