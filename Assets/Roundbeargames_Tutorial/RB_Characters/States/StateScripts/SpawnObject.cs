using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames_tutorial
{
    [CreateAssetMenu(fileName = "SpawnObject", menuName = "Roundbeargames/AbilityData/SpawnObject")]
    public class SpawnObject : StateData
    {
        public PoolObjectType objectType;
        [Range(0f, 1f)]
        public float spawnTiming;
        private bool isSpawned;
        public string parentObjectName = string.Empty;
        public bool stickToParent;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (spawnTiming == 0f)
            {
                CharacterControl control = characterState.GetCharacterControl(animator);
                SpawnObj(control);
                isSpawned = true;
            }
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (!isSpawned)
            {
                if (stateInfo.normalizedTime >= spawnTiming)
                {
                    CharacterControl control = characterState.GetCharacterControl(animator);
                    SpawnObj(control);
                    isSpawned = true;
                }
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            isSpawned = false;
        }

        private void SpawnObj(CharacterControl control)
        {
            GameObject obj = PoolManager.instance.GetObject(objectType);

            if (!string.IsNullOrEmpty(parentObjectName))
            {
                GameObject p = control.GetChildObj(parentObjectName);
                obj.transform.parent = p.transform;
                obj.transform.localPosition = Vector3.zero;
                obj.transform.localRotation = Quaternion.identity;
            }

            if (!stickToParent)
            {
                obj.transform.parent = null;
            }

            obj.SetActive(true);
        }
    }
}
