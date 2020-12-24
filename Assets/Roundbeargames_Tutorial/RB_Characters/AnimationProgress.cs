using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames_tutorial
{
    public class AnimationProgress : MonoBehaviour
    {
        public bool jumped;
        public bool cameraShaken;
        public List<PoolObjectType> poolObjectList = new List<PoolObjectType>();
        //public bool attackTriggered;
        //public float maxPressTime;
        //private float pressTime;

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
    }
}
