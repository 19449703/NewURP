﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "FrameSettings", menuName = "Roundbeargames/Settings/FrameSettings")]
    public class FrameSettings : ScriptableObject
    {
        [Range(0.01f, 1f)]
        public float timeScale;
        public int targetFPS;
    }
}