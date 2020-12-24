using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames_tutorial
{
    public class ManualInput : MonoBehaviour
    {
        private CharacterControl control;

        void Awake()
        {
            control = GetComponent<CharacterControl>();
        }

        // Update is called once per frame
        void Update()
        {
            control.turbo = VirtualInputManager.instance.turbo;
            control.moveUp = VirtualInputManager.instance.moveUp;
            control.moveDown = VirtualInputManager.instance.moveDown;
            control.moveRight = VirtualInputManager.instance.moveRight;
            control.moveLeft = VirtualInputManager.instance.moveLeft;
            control.jump = VirtualInputManager.instance.jump;
            control.attack = VirtualInputManager.instance.attack;
        }
    }
}
