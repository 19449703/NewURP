using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public enum DeathType
    {
        NONE,
        LAUNCH_INTO_AIR,
        GROUND_SHOCK,
    }

    [CreateAssetMenu(fileName = "New DeathAnimationData", menuName = "Roundbeargames/Death/DeathAnimationData")]
    public class DeathAnimationData : ScriptableObject
    {
        public List<GeneralBodyPart> generalBodyParts = new List<GeneralBodyPart>();
        public RuntimeAnimatorController animator;
        public bool isFacingAttacker;
        public DeathType deathType;
    }
}