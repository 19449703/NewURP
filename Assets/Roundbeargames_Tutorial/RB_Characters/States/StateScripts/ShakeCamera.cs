using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames_tutorial
{
    [CreateAssetMenu(fileName = "ShakeCamera", menuName = "Roundbeargames/AbilityData/ShakeCamera")]
    public class ShakeCamera : StateData
    {
        [Range(0f, 0.99f)]
        public float shakeTiming;
        private bool isShaken;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (shakeTiming == 0f)
            {
                CameraManager.instance.ShakeCamera(0.2f);
                isShaken = true;
            }
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (!isShaken)
            {
                if (stateInfo.normalizedTime >= shakeTiming)
                {
                    isShaken = true;
                    CameraManager.instance.ShakeCamera(0.2f);
                }
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            isShaken = false;
        }
    }
}