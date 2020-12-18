using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames_tutorial
{
    public class CharacterState : StateMachineBehaviour
    {
        public List<StateData> listAbilityData = new List<StateData>();

        public void UpdateAll(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            foreach(var d in listAbilityData)
            {
                d.UpdateAbility(characterState, animator, stateInfo);
            }
        }

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			foreach (var d in listAbilityData)
			{
				d.OnEnter(this, animator, stateInfo);
			}
		}

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            UpdateAll(this, animator, stateInfo);
        }

		public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			foreach (var d in listAbilityData)
			{
				d.OnExit(this, animator, stateInfo);
			}
		}

		private CharacterControl control;

        public CharacterControl GetCharacterControl(Animator animator)
        {
            if (control == null)
            {
                control = animator.GetComponentInParent<CharacterControl>();
            }
            return control;
        }
    }

}
