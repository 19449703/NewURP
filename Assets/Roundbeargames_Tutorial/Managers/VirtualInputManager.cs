using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames_tutorial
{
    public class VirtualInputManager : Singleton<VirtualInputManager>
    {
        public bool moveUp = false;
        public bool moveDown = false;
        public bool moveLeft = false;
        public bool moveRight = false;
        public bool jump = false;
        public bool attack = false;
    }

}
