using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Roundbeargames
{
    public class PathFindingAgent : MonoBehaviour
    {
        public bool targetPlayableCharacter;
        public GameObject target;
        NavMeshAgent navMeshAgent;

        List<Coroutine> moveCoroutines = new List<Coroutine>();

        public GameObject startSphere;
        public GameObject endSphere;
        public bool startWalk;

        public CharacterControl owner;

        void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        public void GoToTarget()
        {
            navMeshAgent.enabled = true;
            startSphere.transform.parent = null;
            endSphere.transform.parent = null;

            startWalk = false;
            navMeshAgent.isStopped = false;

            if (targetPlayableCharacter)
            {
                target = CharacterManager.instance.GetPlayableCharacter().gameObject;
            }

            navMeshAgent.SetDestination(target.transform.position);

            if (moveCoroutines.Count != 0)
            {
                if (moveCoroutines[0] != null)
                {
                    StopCoroutine(moveCoroutines[0]);
                }

                moveCoroutines.RemoveAt(0);
            }

            moveCoroutines.Add(StartCoroutine(_Move()));
        }

        IEnumerator _Move()
        {
            while (true)
            {
                if (navMeshAgent.isOnOffMeshLink)
                {
                    owner.navMeshObstacle.carving = true;

                    startSphere.transform.position = navMeshAgent.currentOffMeshLinkData.startPos;
                    endSphere.transform.position = navMeshAgent.currentOffMeshLinkData.endPos;

                    navMeshAgent.CompleteOffMeshLink();
                    
                    navMeshAgent.isStopped = true;
                    startWalk = true;
                    yield break;
                }

                Vector3 dist = transform.position - navMeshAgent.destination;
                if (Vector3.SqrMagnitude(dist) < 0.5f)
                {
                    if (Vector3.SqrMagnitude(owner.transform.position - navMeshAgent.destination) > 1f)
                    {
                        owner.navMeshObstacle.carving = true;
                    }

                    startSphere.transform.position = navMeshAgent.destination;
                    endSphere.transform.position = navMeshAgent.destination;

                    navMeshAgent.isStopped = true;
                    startWalk = true;
                    yield break;
                }

                yield return new WaitForEndOfFrame();
            }
        }
    }
}
