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
        Coroutine moveCoroutine;

        //List<Coroutine> moveCoroutines = new List<Coroutine>();

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

            moveCoroutine = StartCoroutine(_Move());
        }

        public void OnEnable()
        {
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }
        }

        IEnumerator _Move()
        {
            while (true)
            {
                if (navMeshAgent.isOnOffMeshLink)
                {
                    startSphere.transform.position = navMeshAgent.currentOffMeshLinkData.startPos;
                    endSphere.transform.position = navMeshAgent.currentOffMeshLinkData.endPos;

                    navMeshAgent.CompleteOffMeshLink();
                    
                    navMeshAgent.isStopped = true;
                    startWalk = true;
                    break;
                }

                Vector3 dist = transform.position - navMeshAgent.destination;
                if (Vector3.SqrMagnitude(dist) < 0.5f)
                {
                    startSphere.transform.position = navMeshAgent.destination;
                    endSphere.transform.position = navMeshAgent.destination;

                    navMeshAgent.isStopped = true;
                    startWalk = true;
                    break;
                }

                yield return new WaitForEndOfFrame();
            }

            yield return new WaitForSeconds(0.5f);

            owner.navMeshObstacle.carving = true;
        }
    }
}
