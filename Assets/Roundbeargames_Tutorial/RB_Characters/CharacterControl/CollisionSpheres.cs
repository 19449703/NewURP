using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class CollisionSpheres : MonoBehaviour
    {
        public CharacterControl owner;
        public List<GameObject> bottomSpheres = new List<GameObject>();
        public List<GameObject> frontSpheres = new List<GameObject>();
        public List<GameObject> backSpheres = new List<GameObject>();

        public void SetColliderSpheres()
        {
            // bototm
            for (int i = 0; i < 5; i++)
            {
                GameObject obj = Instantiate(Resources.Load<GameObject>("ColliderEdge"), Vector3.zero, Quaternion.identity);
                obj.name = "ColliderEdge-Bottom " + (i + 1).ToString();
                bottomSpheres.Add(obj);
                obj.transform.SetParent(this.transform.Find("Bottom"));
            }

            Reposition_BottomSpheres();

            // front
            for (int i = 0; i < 10; i++)
            {
                GameObject obj = Instantiate(Resources.Load<GameObject>("ColliderEdge"), Vector3.zero, Quaternion.identity);
                obj.name = "ColliderEdge-Front " + (i + 1).ToString();
                frontSpheres.Add(obj);
                obj.transform.SetParent(this.transform.Find("Front"));
            }

            Reposition_FrontSpheres();

            // back
            for (int i = 0; i < 10; i++)
            {
                GameObject obj = Instantiate(Resources.Load<GameObject>("ColliderEdge"), Vector3.zero, Quaternion.identity);
                obj.name = "ColliderEdge-Back " + (i + 1).ToString();
                backSpheres.Add(obj);
                obj.transform.SetParent(this.transform.Find("Back"));
            }

            Reposition_BackSpheres();
        }

        public void Reposition_FrontSpheres()
        {
            float top = owner.boxCollider.bounds.center.y + owner.boxCollider.bounds.size.y * 0.5f;
            float bottom = owner.boxCollider.bounds.center.y - owner.boxCollider.bounds.size.y * 0.5f;
            float front = owner.boxCollider.bounds.center.z + owner.boxCollider.bounds.size.z * 0.5f;

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
            float bottom = owner.boxCollider.bounds.center.y - owner.boxCollider.bounds.size.y * 0.5f;
            float front = owner.boxCollider.bounds.center.z + owner.boxCollider.bounds.size.z * 0.5f;
            float back = owner.boxCollider.bounds.center.z - owner.boxCollider.bounds.size.z * 0.5f;

            bottomSpheres[0].transform.localPosition = new Vector3(0, bottom, back) - this.transform.position;
            bottomSpheres[1].transform.localPosition = new Vector3(0, bottom, front) - this.transform.position;

            float interval = (front - back) / 4;

            for (int i = 2; i < bottomSpheres.Count; i++)
            {
                bottomSpheres[i].transform.localPosition = new Vector3(0, bottom, back + interval * (i - 1)) - this.transform.position;
            }
        }

        public void Reposition_BackSpheres()
        {
            float top = owner.boxCollider.bounds.center.y + owner.boxCollider.bounds.size.y * 0.5f;
            float bottom = owner.boxCollider.bounds.center.y - owner.boxCollider.bounds.size.y * 0.5f;
            float back = owner.boxCollider.bounds.center.z - owner.boxCollider.bounds.size.z * 0.5f;

            backSpheres[0].transform.localPosition = new Vector3(0, bottom + 0.05f, back) - this.transform.position;
            backSpheres[1].transform.localPosition = new Vector3(0, top, back) - this.transform.position;

            float interval = (top - bottom + 0.05f) / 9;

            for (int i = 2; i < backSpheres.Count; i++)
            {
                backSpheres[i].transform.localPosition = new Vector3(0, bottom + interval * (i - 1), back) - this.transform.position;
            }
        }
    }
}