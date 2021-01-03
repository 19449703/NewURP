using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "GroundDetector", menuName = "Roundbeargames/AbilityData/GroundDetector")]
    public class GroundDetector : StateData
    {
        [Range(0.01f, 1f)]
        public float checkTime;
        public float distance;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.characterControl;

            if (stateInfo.normalizedTime >= checkTime)
            {
                animator.SetBool(TransitionParameter.Grounded.ToString(), IsGrounded(control));
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
        }

        bool IsGrounded(CharacterControl control)
        {
            if (control.contactPoints != null)
            {
                float colliderBottom = control.transform.position.y +
                    control.boxCollider.center.y -
                    control.boxCollider.size.y * 0.5f;

                foreach (ContactPoint c in control.contactPoints)
                {
                    float yDiff = Mathf.Abs(c.point.y - colliderBottom);
                    if (yDiff < 0.01f)
                    {
                        if (Mathf.Abs(control.RIGID_BODY.velocity.y) < 0.001f)
                        {
                            return true;
                        }
                    } 
                }
            }

            if (control.RIGID_BODY.velocity.y < 0f)
            {
                foreach (var o in control.bottomSpheres)
                {
                    Debug.DrawRay(o.transform.position, -Vector3.up * 0.7f, Color.yellow);
                    RaycastHit hit;
                    if (Physics.Raycast(o.transform.position, -Vector3.up, out hit, distance))
                    {
                        if (!control.ragdollParts.Contains(hit.collider)
                            && !Ledge.IsLedge(hit.collider.gameObject)
                            && !Ledge.IsLedgeChecker(hit.collider.gameObject)
                            && !Ledge.IsCharacter(hit.collider.gameObject)
                            )
                        {
                            return true;
                        }
                    }
                }
            }
            
            return false;
        }
    }
}
