using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public enum CameraTrigger
    {
        Default, 
        Shake,
    }

    public class CameraController : MonoBehaviour
    {
        private Animator animator;
        public Animator ANIMATOR
        {
            get
            {
                if (animator == null)
                {
                    animator = GetComponent<Animator>();
                }

                return animator;
            }
        }

        public void TriggerCamera(CameraTrigger trigger)
        {
            Debug.Log(trigger.ToString());
            ANIMATOR.SetTrigger(trigger.ToString());
        }
    }
}
