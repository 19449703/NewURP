using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public enum PoolObjectType
    {
        ATTACKINFO,
        HAMMER_OBJ,
        HAMMER_VFX,
    }

    public class PoolObjectLoader : MonoBehaviour
    {
        public static PoolObject InstantiatePrefab(PoolObjectType objType)
        {
            GameObject obj = null;
            GameObject prefab = null;

            switch (objType)
            {
                case PoolObjectType.ATTACKINFO:
                    {
                        prefab = Resources.Load("AttackInfo", typeof(GameObject)) as GameObject;
                        break;
                    }
                case PoolObjectType.HAMMER_OBJ:
                    {
                        prefab = Resources.Load("ThorHammer", typeof(GameObject)) as GameObject;
                        break;
                    }
                case PoolObjectType.HAMMER_VFX:
                    {
                        prefab = Resources.Load("VFX_HammerDown", typeof(GameObject)) as GameObject;
                        break;
                    }
            }

            obj = Instantiate(prefab);
            return obj.GetComponent<PoolObject>();
        }
    }
}