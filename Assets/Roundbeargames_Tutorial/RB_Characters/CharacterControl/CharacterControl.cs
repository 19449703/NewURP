using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames_tutorial
{
	public enum TransitionParameter
	{
		Move,
        Jump,
        ForceTransition,
        Grounded,
        Attack,
        ClickAnimation,
        TransitionIndex,
        Turbo,
        Turn,
	}

    public enum RBScenes
    {
        TutorialScene_CharacterSelect,
        TutorialScene_Sample,
    }

	public class CharacterControl : MonoBehaviour
    {
        public PlayableCharacterType playableCharacterType;
        public Animator skinedMeshAnimator;

        public bool turbo = false;
        public bool moveUp = false;
        public bool moveDown = false;
        public bool moveLeft = false;
        public bool moveRight = false;
        public bool jump = false;
        public bool attack = false;
        public LedgeChecker ledgeChecker;
        public AnimationProgress animationProgress;

        private Rigidbody rigid;
        public Rigidbody RIGID_BODY
        {
            get
            {
                if (rigid == null)
                {
                    rigid = GetComponent<Rigidbody>();
                }
                return rigid;
            }
        }

        public GameObject colliderEdgePrefab;
        public List<GameObject> bottomSpheres = new List<GameObject>();
        public List<GameObject> frontSpheres = new List<GameObject>();
        public List<Collider> ragdollParts = new List<Collider>();

        private List<TriggerDetector> triggerDetectors = new List<TriggerDetector>();
        private Dictionary<string, GameObject> childObjects = new Dictionary<string, GameObject>();

        public float gravityMultiplier;
        public float pullMultiplier;

        private void Awake()
        {
            bool switchBack = false;

            if (!IsFaceingForward())
            {
                switchBack = true;
            }

            FaceForward(true);
            SetColliderSpheres();
            

            if (switchBack)
            {
                FaceForward(false);
            }

            ledgeChecker = GetComponentInChildren<LedgeChecker>();
            animationProgress = GetComponent<AnimationProgress>();

            RegisterCharacter();
        }

        private void RegisterCharacter()
        {
            if (!CharacterManager.instance.characters.Contains(this))
            {
                CharacterManager.instance.characters.Add(this);
            }
        }

        public List<TriggerDetector> GetAllTriggers()
        {
            if (triggerDetectors.Count == 0)
            {
                TriggerDetector[] arr = gameObject.GetComponentsInChildren<TriggerDetector>();
                foreach(var trigger in arr)
                {
                    triggerDetectors.Add(trigger);
                }
            }

            return triggerDetectors;
        }

        //private IEnumerator Start()
        //{
        //    yield return new WaitForSeconds(5f);
        //    RIGID_BODY.AddForce(300 * Vector3.up);
        //    yield return new WaitForSeconds(0.5f);
        //    TurnOnRagdoll();
        //}

        public void SetRiagdollParts()
        {
            ragdollParts.Clear();

            Collider[] colliders = this.gameObject.GetComponentsInChildren<Collider>();

            foreach(Collider c in colliders)
            {
                if (c.gameObject != this.gameObject)
                {
                    c.isTrigger = true;
                    ragdollParts.Add(c);

                    if (c.GetComponent<TriggerDetector>() == null)
                        c.gameObject.AddComponent<TriggerDetector>();
                }
            }
        }

        public void TurnOnRagdoll()
        {
            RIGID_BODY.useGravity = false;
            RIGID_BODY.velocity = Vector3.zero;
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            skinedMeshAnimator.enabled = false;
            skinedMeshAnimator.avatar = null;

            foreach (Collider c in ragdollParts)
            {
                c.isTrigger = false;
                c.attachedRigidbody.velocity = Vector3.zero;
            }
        }

        private void SetColliderSpheres()
        {
            BoxCollider box = GetComponent<BoxCollider>();

            float top = box.bounds.center.y + box.bounds.extents.y;
            float bottom = box.bounds.center.y - box.bounds.extents.y;
            float front = box.bounds.center.z + box.bounds.extents.z;
            float back = box.bounds.center.z - box.bounds.extents.z;

            GameObject bottomFront = CreateEdgeSphere(new Vector3(0, bottom, front));
            GameObject bottomBack = CreateEdgeSphere(new Vector3(0, bottom, back));
            bottomFront.transform.SetParent(this.transform);
            bottomBack.transform.SetParent(this.transform);
            bottomSpheres.Add(bottomFront);
            bottomSpheres.Add(bottomBack);

            GameObject topFront = CreateEdgeSphere(new Vector3(0, top, front));
            topFront.transform.SetParent(this.transform);
            frontSpheres.Add(topFront);

            float horSec = Vector3.Distance(bottomFront.transform.localPosition, bottomBack.transform.localPosition) / 5;
            CreateMiddleSpheres(bottomFront, -this.transform.forward, horSec, 4, bottomSpheres);

            float verSec = Vector3.Distance(bottomFront.transform.localPosition, topFront.transform.localPosition) / 10;
            CreateMiddleSpheres(bottomFront, this.transform.up, verSec, 9, frontSpheres);
        }

        private void FixedUpdate()
        {
            if (RIGID_BODY.velocity.y < 0f)
            {
                //Debug.Log("gravity");
                RIGID_BODY.velocity += -Vector3.up * gravityMultiplier;
            }

            if (RIGID_BODY.velocity.y > 0f && !jump)
            {
                //Debug.Log("pull");
                RIGID_BODY.velocity += -Vector3.up * pullMultiplier;
            }
        }
        public void CreateMiddleSpheres(GameObject start, Vector3 dir, float sec,
            int iterations, List<GameObject> spheresList)
        {
            for (int i = 0; i < iterations; i++)
            {
                Vector3 pos = start.transform.position + dir * sec * (i + 1);
                GameObject obj = CreateEdgeSphere(pos);
                obj.transform.SetParent(this.transform);
                spheresList.Add(obj);
            }
        }

        public GameObject CreateEdgeSphere(Vector3 pos)
        {
            GameObject obj = Instantiate(colliderEdgePrefab, pos, Quaternion.identity);
            return obj;
        }

        public void MoveForward(float speed, float speedGraph)
        {
            this.transform.Translate(Vector3.forward * speed * speedGraph * Time.deltaTime);
        }

        public void FaceForward(bool forward)
        {
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.Equals(RBScenes.TutorialScene_CharacterSelect.ToString()))
            {
                return;
            }

            this.transform.rotation = Quaternion.Euler(0, forward ? 0 : 180, 0);
        }

        public bool IsFaceingForward()
        {
            return this.transform.forward.z > 0;
        } 

        public Collider GetBodyPart(string name)
        {
            foreach (Collider c in ragdollParts)
            {
                if (c.name.Contains(name))
                    return c;
            }
            return null;
        }

        public GameObject GetChildObj(string name)
        {
            if (childObjects.ContainsKey(name))
            {
                return childObjects[name];
            }

            Transform[] arr = this.gameObject.GetComponentsInChildren<Transform>();
            foreach(var t in arr)
            {
                if (t.gameObject.name.Equals(name))
                {
                    childObjects.Add(name, t.gameObject);
                    return t.gameObject;
                }
            }

            return null;
        }
    }
}
