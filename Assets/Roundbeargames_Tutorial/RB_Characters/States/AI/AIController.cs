using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
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
        Coroutine aiRoutine;

        void Start()
        {
            InitializeAI();
        }

        public void InitializeAI()
        {
            if (aiList.Count == 0)
            {
                AISubset[] arr = gameObject.GetComponentsInChildren<AISubset>();

                foreach (var s in arr)
                {
                    if (!aiList.Contains(s))
                    {
                        aiList.Add(s);
                        s.gameObject.SetActive(false);
                    }
                }
            }

            aiRoutine = StartCoroutine(_InitAI());
        }

        private void OnEnable()
        {
            if (aiRoutine != null)
            {
                StopCoroutine(aiRoutine);
            }
        }

        private IEnumerator _InitAI()
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
