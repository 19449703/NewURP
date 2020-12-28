using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames_tutorial
{
    public enum GeneralBodyPart
    {
        Upper,
        Lower,
        Arm,
        Leg,
    }

    public class TriggerDetector : MonoBehaviour
    {
        public GeneralBodyPart generalBodyPart;

        public List<Collider> collidingParts = new List<Collider>();
        private CharacterControl owner;

        public Vector3 lastPosition;
        public Quaternion lastRotation;
        private void Awake()
        {
            owner = this.GetComponentInParent<CharacterControl>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (owner.ragdollParts.Contains(other))
                return;

            CharacterControl attacker = other.transform.root.GetComponent<CharacterControl>();
            if (attacker == null)
                return;

            if (attacker.gameObject == other.gameObject)
                return;

            if (!collidingParts.Contains(other))
            {
                collidingParts.Add(other);
            }
        }

        private void OnTriggerExit(Collider attacker)
        {
            if (collidingParts.Contains(attacker))
            {
                collidingParts.Remove(attacker);
            }
        }
    }
}
