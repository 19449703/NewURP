using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "OffsetOnLedge", menuName = "Roundbeargames/AbilityData/OffsetOnLedge")]
    public class OffsetOnLedge : StateData
    {
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.characterControl;
            GameObject anim = control.skinedMeshAnimator.gameObject;
            anim.transform.SetParent(control.ledgeChecker.grabbedLedge.transform);
            anim.transform.localPosition = control.ledgeChecker.grabbedLedge.offset;
            anim.transform.SetParent(control.transform, true);
            anim.transform.SetAsFirstSibling();

            control.RIGID_BODY.velocity = Vector3.zero;
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
        }
    }
}