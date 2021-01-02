using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class LedgeChecker : MonoBehaviour
    {
        public bool isGrabbingLedge;
        public Ledge grabbedLedge;
        private Ledge checkLedge = null;

        private void OnTriggerEnter(Collider other)
        {
            checkLedge = other.gameObject.GetComponent<Ledge>();
            if (checkLedge != null)
            {
                isGrabbingLedge = true;
                grabbedLedge = checkLedge;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            checkLedge = other.gameObject.GetComponent<Ledge>();
            if (checkLedge != null)
            {
                isGrabbingLedge = false;
                grabbedLedge = null;
            }
        }
    }
}
