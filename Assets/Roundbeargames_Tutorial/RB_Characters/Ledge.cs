using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames_tutorial
{
    public class Ledge : MonoBehaviour
    {
        public Vector3 offset;  // animator所在的GameObject相对于Ledge的局部坐标
        public Vector3 endPosition;

        public static bool IsLedge(GameObject obj)
        {
            if (obj.GetComponent<Ledge>() == null)
            {
                return false;
            }

            return true;
        }
    }
}