using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames_tutorial
{
    [CreateAssetMenu(fileName = "OffsetOnLedge", menuName = "Roundbeargames/AbilityData/OffsetOnLedge")]
    public class OffsetOnLedge : StateData
    {
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);
            GameObject anim = control.skinedMeshAnimator.gameObject;
            anim.transform.SetParent(control.ledgeChecker.grabbedLedge.transform);
            anim.transform.localPosition = control.ledgeChecker.grabbedLedge.offset;

            control.RIGID_BODY.velocity = Vector3.zero;
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            // 在TeleportOnLedge里实现
            //CharacterControl control = characterState.GetCharacterControl(animator);
            //GameObject anim = control.skinedMeshAnimator.gameObject;
            //anim.transform.SetParent(control.transform);
        }
    }
}