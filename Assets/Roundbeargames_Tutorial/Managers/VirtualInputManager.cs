using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames_tutorial
{
    public class VirtualInputManager : Singleton<VirtualInputManager>
    {
        public bool moveLeft;
        public bool moveRight;
        public bool jump;
        public bool attack;
    }

}
