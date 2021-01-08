using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "ToggleBoxCollider", menuName = "Roundbeargames/AbilityData/ToggleBoxCollider")]
    public class ToggleBoxCollider : StateData
    {
        public bool on;
        public bool onStart;
        public bool onEnd;

        [Space(10)]
        public bool repositionSpheres;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (onStart)
            {
                CharacterControl control = characterState.characterControl;
                ToggleBoxCol(control);
            }
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (onEnd)
            {
                CharacterControl control = characterState.characterControl;
                ToggleBoxCol(control);
            }
        }

        private void ToggleBoxCol(CharacterControl control)
        {
            control.boxCollider.enabled = on;

            if (repositionSpheres)
            {
                control.collisionSpheres.Reposition_FrontSpheres();
                control.collisionSpheres.Reposition_BottomSpheres();
                control.collisionSpheres.Reposition_BackSpheres();
            }
        }
    }
}