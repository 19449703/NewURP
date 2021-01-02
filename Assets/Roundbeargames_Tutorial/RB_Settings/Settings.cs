using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class Settings : MonoBehaviour
    {
        public FrameSettings frameSettings;
        public PhysicsSettings physicsSettings;

        private void Awake()
        {
            Time.timeScale = frameSettings.timeScale;
            Application.targetFrameRate = frameSettings.targetFPS;
            Physics.defaultSolverVelocityIterations = physicsSettings.defaultSolverVelocityIterations;
            VirtualInputManager.instance.LoadKeys();
        }
    }
}