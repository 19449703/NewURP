using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class CharacterState : StateMachineBehaviour
    {
        public List<StateData> listAbilityData = new List<StateData>();
        public CharacterControl characterControl;

        public void UpdateAll(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            foreach(var d in listAbilityData)
            {
                d.UpdateAbility(characterState, animator, stateInfo);
            }
        }

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
            if (characterControl == null)
            {
                CharacterControl control = animator.transform.root.GetComponent<CharacterControl>();
                control.CacheCharacterControl(animator);
            }

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
    }
}
