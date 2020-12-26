using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames_tutorial
{
    [CreateAssetMenu(fileName = "TeleportOnLedge", menuName = "Roundbeargames/AbilityData/TeleportOnLedge")]
    public class TeleportOnLedge : StateData
    {
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            // 此时不能使用characterState.GetCharacterControl，因为animator所在的GameObject在Ledge下面挂着
            // CharacterControl control = CharacterControl control = characterState.GetCharacterControl(animator);
            CharacterControl control = CharacterManager.instance.GetCharacter(animator);

            Vector3 endPosition = control.ledgeChecker.grabbedLedge.transform.position + control.ledgeChecker.grabbedLedge.endPosition;

            control.transform.position = endPosition;
            control.skinedMeshAnimator.transform.position = endPosition;
            control.skinedMeshAnimator.transform.SetParent(control.transform);
        }
    }
}