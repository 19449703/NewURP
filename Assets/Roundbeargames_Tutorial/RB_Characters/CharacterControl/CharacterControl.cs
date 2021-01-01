using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
        [Header("Input")]
        public bool turbo = false;
        public bool moveUp = false;
        public bool moveDown = false;
        public bool moveLeft = false;
        public bool moveRight = false;
        public bool jump = false;
        public bool attack = false;

        [Header("SubComponents")]
        public LedgeChecker ledgeChecker;
        public AnimationProgress animationProgress;
        public AIProgress aiProgress;
        public DamageDecetor damageDetector;
        public List<GameObject> bottomSpheres = new List<GameObject>();
        public List<GameObject> frontSpheres = new List<GameObject>();
        public AIController aiController;
        public BoxCollider boxCollider;
        public NavMeshObstacle navMeshObstacle;

        [Header("Gravity")]
        public float gravityMultiplier;
        public float pullMultiplier;
        public ContactPoint[] contactPoints;

        [Header("Setup")]
        public PlayableCharacterType playableCharacterType;
        public Animator skinedMeshAnimator;
        public List<Collider> ragdollParts = new List<Collider>();
        public GameObject leftHand_Attack;
        public GameObject rightHand_Attack;

        private List<TriggerDetector> triggerDetectors = new List<TriggerDetector>();
        private Dictionary<string, GameObject> childObjects = new Dictionary<string, GameObject>();

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

        private void Awake()
        {
            ledgeChecker = GetComponentInChildren<LedgeChecker>();
            animationProgress = GetComponent<AnimationProgress>();
            aiProgress = GetComponentInChildren<AIProgress>();
            damageDetector = GetComponentInChildren<DamageDecetor>();
            aiController = GetComponentInChildren<AIController>();
            boxCollider = GetComponent<BoxCollider>();
            navMeshObstacle = GetComponent<NavMeshObstacle>();

            SetColliderSpheres();
            RegisterCharacter();
        }

        private void OnCollisionStay(Collision collision)
        {
            contactPoints = collision.contacts;
        }

        private void OnCollisionExit(Collision collision)
        {
            contactPoints = null;
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

        public void SetRiagdollParts()
        {
            ragdollParts.Clear();

            Collider[] colliders = this.gameObject.GetComponentsInChildren<Collider>();

            foreach(Collider c in colliders)
            {
                if (c.gameObject != this.gameObject)
                {
                    if (c.gameObject.GetComponent<LedgeChecker>() == null)
                    {
                        c.isTrigger = true;
                        ragdollParts.Add(c);
                        c.attachedRigidbody.interpolation = RigidbodyInterpolation.Interpolate;
                        c.attachedRigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

                        CharacterJoint joint = c.GetComponent<CharacterJoint>();
                        if (joint != null)
                        {
                            joint.enableProjection = true;
                        }

                        if (c.GetComponent<TriggerDetector>() == null)
                            c.gameObject.AddComponent<TriggerDetector>();
                    }
                }
            }
        }

        public void TurnOnRagdoll()
        {
            // change layers
            Transform[] arr = GetComponentsInChildren<Transform>();
            foreach(Transform t in arr)
            {
                t.gameObject.layer = LayerMask.NameToLayer(RB_Layers.DEADBODY.ToString());
            }

            // save bodypart positions
            foreach (Collider c in ragdollParts)
            {
                TriggerDetector det = c.GetComponent<TriggerDetector>();
                det.lastPosition = c.gameObject.transform.localPosition;
                det.lastRotation = c.gameObject.transform.localRotation;
            }

            // turn off animator/avatar/etc
            RIGID_BODY.useGravity = false;
            RIGID_BODY.velocity = Vector3.zero;
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            skinedMeshAnimator.enabled = false;
            skinedMeshAnimator.avatar = null;

            // turn on ragdoll
            foreach (Collider c in ragdollParts)
            {
                c.isTrigger = false;

                TriggerDetector det = c.GetComponent<TriggerDetector>();
                c.transform.localPosition = det.lastPosition;
                c.transform.localRotation = det.lastRotation;

                c.attachedRigidbody.velocity = Vector3.zero;
            }
        }

        private void SetColliderSpheres()
        {
            for (int i = 0; i < 5; i++)
            {
                GameObject obj = Instantiate(Resources.Load<GameObject>("ColliderEdge"), Vector3.zero, Quaternion.identity);
                obj.name = "ColliderEdge-Bottom " + (i + 1).ToString();
                bottomSpheres.Add(obj);
                obj.transform.SetParent(this.transform);
            }

            Reposition_BottomSpheres();

            for (int i = 0; i < 10; i++)
            {
                GameObject obj = Instantiate(Resources.Load<GameObject>("ColliderEdge"), Vector3.zero, Quaternion.identity);
                obj.name = "ColliderEdge-Front " + (i + 1).ToString();
                frontSpheres.Add(obj);
                obj.transform.SetParent(this.transform);
            }

            Reposition_FrontSpheres();
        }

        public void Reposition_FrontSpheres()
        {
            float top = boxCollider.bounds.center.y + boxCollider.bounds.size.y * 0.5f;
            float bottom = boxCollider.bounds.center.y - boxCollider.bounds.size.y * 0.5f;
            float front = boxCollider.bounds.center.z + boxCollider.bounds.size.z * 0.5f;

            frontSpheres[0].transform.localPosition = new Vector3(0, bottom + 0.05f, front) - this.transform.position;
            frontSpheres[1].transform.localPosition = new Vector3(0, top, front) - this.transform.position;

            float interval = (top - bottom + 0.05f) / 9;
            
            for (int i = 2; i < frontSpheres.Count; i++)
            {
                frontSpheres[i].transform.localPosition = new Vector3(0, bottom + interval * (i - 1), front) - this.transform.position;
            }
        }

        public void Reposition_BottomSpheres()
        {
            float bottom = boxCollider.bounds.center.y - boxCollider.bounds.size.y * 0.5f;
            float front = boxCollider.bounds.center.z + boxCollider.bounds.size.z * 0.5f;
            float back = boxCollider.bounds.center.z - boxCollider.bounds.size.z * 0.5f;

            bottomSpheres[0].transform.localPosition = new Vector3(0, bottom, back) - this.transform.position;
            bottomSpheres[1].transform.localPosition = new Vector3(0, bottom, front) - this.transform.position;

            float interval = (front - back) / 4;

            for (int i = 2; i < bottomSpheres.Count; i++)
            {
                bottomSpheres[i].transform.localPosition = new Vector3(0, bottom, back + interval * (i - 1)) - this.transform.position;
            }
        }

        public void UpdateBoxCollider_Size()
        {
            if (!animationProgress.updatingBoxCollider)
                return;

            if (Vector3.SqrMagnitude(boxCollider.size - animationProgress.targetSize) > 0.01f)
            {
                boxCollider.size = Vector3.Lerp(boxCollider.size, animationProgress.targetSize,
                    animationProgress.sizeSpeed * Time.deltaTime);

                animationProgress.updatingSpheres = true;
            }
        }

        public void UpdateBoxCollider_Center()
        {
            if (!animationProgress.updatingBoxCollider)
                return;

            if (Vector3.SqrMagnitude(boxCollider.center - animationProgress.targetCenter) > 0.01f)
            {
                boxCollider.center = Vector3.Lerp(boxCollider.center, animationProgress.targetCenter,
                    animationProgress.centerSpeed * Time.deltaTime);

                animationProgress.updatingSpheres = true;
            }
        }

        private void FixedUpdate()
        {
            if (RIGID_BODY.velocity.y < 0f)
            {
                RIGID_BODY.velocity += -Vector3.up * gravityMultiplier;
            }

            if (RIGID_BODY.velocity.y > 0f && !jump)
            {
                RIGID_BODY.velocity += -Vector3.up * pullMultiplier;
            }

            animationProgress.updatingSpheres = false;
            UpdateBoxCollider_Size();
            UpdateBoxCollider_Center();
            if (animationProgress.updatingSpheres)
            {
                Reposition_BottomSpheres();
                Reposition_FrontSpheres();
            }

            if (animationProgress.ragdollTriggered)
            {
                TurnOnRagdoll();
                animationProgress.ragdollTriggered = false;
            }
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
