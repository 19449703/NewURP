using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "SavedKeys", menuName = "Roundbeargames/Settings/SavedKeys")]
    public class SavedKeys : ScriptableObject
    {
        public List<KeyCode> keyCodesList = new List<KeyCode>();
    }
}