using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class AnimationProgress : MonoBehaviour
    {
        public List<StateData> currentRunningAbilities = new List<StateData>();

        public bool cameraShaken;
        public List<PoolObjectType> poolObjectList = new List<PoolObjectType>();
		public bool ragdollTriggered;
        //public bool attackTriggered;
        //public float maxPressTime;
        //private float pressTime;

        [Header("GroundMovement")]
        public bool disallowEarlyTurn;
        public bool lockDirectionNextState;

        [Header("AirCondition")]
        public bool jumped;
        public float airMomentum;
        public bool cancelPull;

        [Header("UpdateBoxCollider")]
        public bool updatingBoxCollider;
        public bool updatingSpheres;
        public Vector3 targetSize;
        public float sizeSpeed;
        public Vector3 targetCenter;
        public float centerSpeed;

        private CharacterControl control;

        private void Awake()
        {
            control = GetComponent<CharacterControl>();
        }

        private void Update()
        {
            //if (control.attack)
            //{
            //    pressTime += Time.deltaTime;
            //}
            //else
            //{
            //    pressTime = 0;
            //}

            //if (pressTime == 0f || pressTime > maxPressTime)
            //{
            //    attackTriggered = false;
            //}
            //else
            //{
            //    attackTriggered = true;
            //}
        }

        private void LateUpdate()
        {
            
        }

        public bool IsRunning(System.Type type, StateData self)
        {
            for (int i = 0; i < currentRunningAbilities.Count; i++)
            {
                if (type == currentRunningAbilities[i].GetType())
                {
                    if (currentRunningAbilities[i] == self)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
