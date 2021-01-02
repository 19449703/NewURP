using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class AttackManager : Singleton<AttackManager>
    {
        public List<AttackInfo> currentAttcks = new List<AttackInfo>();
    }
}