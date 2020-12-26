using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace roundbeargames_tutorial
{
    public class PathFindingAgent : MonoBehaviour
    {
        public bool targetPlayableCharacter;
        public GameObject target;
        NavMeshAgent navMeshAgent;

        Coroutine move;
        public Vector3 startPosition;
        public Vector3 endPosition;
        public GameObject startSphere;
        public GameObject endSphere;

        void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        public void GoToTarget()
        {
            startSphere.transform.parent = null;
            endSphere.transform.parent = null;

            navMeshAgent.isStopped = false;

            if (targetPlayableCharacter)
            {
                target = CharacterManager.instance.GetPlayableCharacter().gameObject;
            }

            navMeshAgent.SetDestination(target.transform.position);

            if (move != null)
            {
                StopCoroutine(move);
            }

            move = StartCoroutine(_Move());
        }

        IEnumerator _Move()
        {
            while (true)
            {
                if (navMeshAgent.isOnOffMeshLink)
                {
                    startPosition = transform.position;
                    startSphere.transform.position = startPosition;
                    navMeshAgent.CompleteOffMeshLink();

                    yield return new WaitForEndOfFrame();
                    endPosition = transform.position;
                    endSphere.transform.position = endPosition;
                    navMeshAgent.isStopped = true;
                    yield break;
                }

                Vector3 dist = transform.position - navMeshAgent.destination;
                if (Vector3.SqrMagnitude(dist) < 0.5f)
                {
                    startPosition = transform.position;
                    endPosition = transform.position;
                    navMeshAgent.isStopped = true;
                    yield break;
                }

                yield return new WaitForEndOfFrame();
            }
        }
    }
}
