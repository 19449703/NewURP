using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class PlayerInput : MonoBehaviour
    {
        public SavedKeys savedKeys;

        void Update()
        {
            VirtualInputManager.instance.turbo = Input.GetKey(VirtualInputManager.instance.dicKeys[InputKeyType.KEY_TURBO]);
            VirtualInputManager.instance.moveUp = Input.GetKey(VirtualInputManager.instance.dicKeys[InputKeyType.KEY_MOVE_UP]);
            VirtualInputManager.instance.moveDown = Input.GetKey(VirtualInputManager.instance.dicKeys[InputKeyType.KEY_MOVE_DOWN]);
            VirtualInputManager.instance.moveLeft = Input.GetKey(VirtualInputManager.instance.dicKeys[InputKeyType.KEY_MOVE_LEFT]);
            VirtualInputManager.instance.moveRight = Input.GetKey(VirtualInputManager.instance.dicKeys[InputKeyType.KEY_MOVE_RIGHT]);
            VirtualInputManager.instance.jump = Input.GetKey(VirtualInputManager.instance.dicKeys[InputKeyType.KEY_JUMP]);
            VirtualInputManager.instance.attack = Input.GetKeyDown(VirtualInputManager.instance.dicKeys[InputKeyType.KEY_ATTACK]);
        }
    }

}
