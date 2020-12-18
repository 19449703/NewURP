using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames_tutorial
{
    public class AttackManager : Singleton<AttackManager>
    {
        public List<AttackInfo> currentAttcks = new List<AttackInfo>();
    }
}