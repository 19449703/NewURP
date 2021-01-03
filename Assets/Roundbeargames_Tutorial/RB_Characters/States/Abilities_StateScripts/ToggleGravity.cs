using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "ToggleGravity", menuName = "Roundbeargames/AbilityData/ToggleGravity")]
    public class ToggleGravity : StateData
    {
        public bool on;
        public bool onStart;
        public bool onEnd;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (onStart)
            {
                CharacterControl control = characterState.characterControl;
                ToggleGrav(control);
            }
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (onEnd)
            {
                ToggleGrav(characterState.characterControl);
            }
        }

        private void ToggleGrav(CharacterControl control)
        {
            control.RIGID_BODY.velocity = Vector3.zero;
            control.RIGID_BODY.useGravity = on;
        }
    }
}