using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames_tutorial
{
    public class PoolObject : MonoBehaviour
    {
        public PoolObjectType poolObjectType;
        public float scheduledOffTime;
        private Coroutine offRoutine;

        private void OnEnable()
        {
            if (offRoutine != null)
            {
                StopCoroutine(offRoutine);
            }

            if (scheduledOffTime > 0)
                offRoutine = StartCoroutine(_ScheduledOff());
        }

        public void TurnOff()
        {
            PoolManager.instance.AddObject(this);
            gameObject.SetActive(false);
        }

        IEnumerator _ScheduledOff()
        {
            yield return new WaitForSeconds(scheduledOffTime);

            if (!PoolManager.instance.poolDictionary[poolObjectType].Contains(this.gameObject))
            {
                TurnOff();
            }
        }
    }
}
