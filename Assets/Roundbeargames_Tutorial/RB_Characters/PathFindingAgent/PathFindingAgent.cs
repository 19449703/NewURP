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

        void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        public void GoToTarget()
        {
            if (targetPlayableCharacter)
            {
                target = CharacterManager.instance.GetPlayableCharacter().gameObject;
            }

            navMeshAgent.SetDestination(target.transform.position);
        }
    }

}
