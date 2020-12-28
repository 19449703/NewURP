using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames_tutorial
{
    public enum AI_Type
    {
        WALK_AND_JUMP,
        RUN,
    }

    public class AIController : MonoBehaviour
    {
        public List<AISubset> aiList = new List<AISubset>();
        public AI_Type initialAI;

        void Awake()
        {
            AISubset[] arr = gameObject.GetComponentsInChildren<AISubset>();

            foreach(var s in arr)
            {
                if (!aiList.Contains(s))
                {
                    aiList.Add(s);
                    s.gameObject.SetActive(false);
                }
            }
        }

        private IEnumerator Start()
        {
            yield return new WaitForEndOfFrame();

            TriggerAI(initialAI);
        }

        public void TriggerAI(AI_Type type)
        {
            AISubset next = null;

            foreach(var s in aiList)
            {
                s.gameObject.SetActive(false);
                if (s.aiType == type)
                {
                    next = s;
                }
            }

            if (next != null)
			{
				next.gameObject.SetActive(true);
			}
        }
    }
}
