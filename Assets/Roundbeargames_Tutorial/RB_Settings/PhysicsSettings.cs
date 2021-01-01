using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames_tutorial
{
    [CreateAssetMenu(fileName = "PhysicsSettings", menuName = "Roundbeargames/Settings/PhysicsSettings")]
    public class PhysicsSettings : ScriptableObject
    {
        public int defaultSolverVelocityIterations;
    }
}