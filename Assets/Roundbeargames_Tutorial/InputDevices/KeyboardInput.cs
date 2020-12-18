using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames_tutorial
{
    public class KeyboardInput : MonoBehaviour
    {
        void Update()
        {
            VirtualInputManager.instance.moveLeft = Input.GetKey(KeyCode.A);
            VirtualInputManager.instance.moveRight = Input.GetKey(KeyCode.D);
            VirtualInputManager.instance.jump = Input.GetKey(KeyCode.Space);
            VirtualInputManager.instance.attack = Input.GetKeyDown(KeyCode.Return);
        }
    }

}
