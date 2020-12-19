using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames_tutorial
{
    [CreateAssetMenu(fileName = "New DeathAnimationData", menuName = "Roundbeargames/Death/DeathAnimationData")]
    public class DeathAnimationData : ScriptableObject
    {
        public List<GeneralBodyPart> generalBodyParts = new List<GeneralBodyPart>();
        public RuntimeAnimatorController animator;
        public bool isFacingAttacker;
        public bool launchIntoAir;
    }
}