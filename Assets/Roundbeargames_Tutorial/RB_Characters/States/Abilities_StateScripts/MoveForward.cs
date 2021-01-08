using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    [CreateAssetMenu(fileName = "MoveForward", menuName = "Roundbeargames/AbilityData/MoveForward")]
    public class MoveForward : StateData
    {
        public bool allowEarlyTurn;
        public bool lockDirection;
        public bool lockDirectionNextState;
        public bool constant;
        public AnimationCurve speedGraph;
        public float speed;
        public float blockDistance;
        public bool ignoreCharacterBox;

        private List<GameObject> sphereList;
        private float dirBlock;

        [Header("Momentum")]
        public bool useMomentum;
        public float startingMomentum;
        public float maxMomentum;
        public bool clearMomentumOnExit;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.characterControl;

            if (allowEarlyTurn && !control.animationProgress.disallowEarlyTurn)
            {
                if (!control.animationProgress.lockDirectionNextState)
                {
                    if (control.moveLeft)
                    {
                        control.FaceForward(false);
                    }
                    else if (control.moveRight)
                    {
                        control.FaceForward(true);
                    }
                }
                else
                {
                    control.animationProgress.lockDirectionNextState = false;
                }
            }

            control.animationProgress.disallowEarlyTurn = false;

            if (startingMomentum > 0.001f)
            {
                if (control.IsFaceingForward())
                {
                    control.animationProgress.airMomentum = startingMomentum;
                }
                else
                {
                    control.animationProgress.airMomentum = -startingMomentum;
                }
            }
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.characterControl;

            control.animationProgress.lockDirectionNextState = lockDirectionNextState;

            if (control.animationProgress.IsRunning(typeof(MoveForward), this))
            {
                return;
            }

            if (control.jump)
            {
                animator.SetBool(TransitionParameter.Jump.ToString(), true);
            }

            if (useMomentum)
            {
                UpdateMomentum(control, stateInfo);
            }
            else
            {
                if (constant)
                {
                    ConstentMove(control, animator, stateInfo);
                }
                else
                {
                    ControlledMove(control, animator, stateInfo);
                }
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.characterControl;

            if (clearMomentumOnExit)
            {
                control.animationProgress.airMomentum = 0;
            }
        }

        public void UpdateMomentum(CharacterControl control, AnimatorStateInfo stateInfo)
        {
            if (control.moveLeft)
            {
                control.animationProgress.airMomentum -= speedGraph.Evaluate(stateInfo.normalizedTime) * speed * Time.deltaTime;
            }

            if (control.moveRight)
            {
                control.animationProgress.airMomentum += speedGraph.Evaluate(stateInfo.normalizedTime) * speed * Time.deltaTime;
            }

            control.animationProgress.airMomentum = Mathf.Clamp(control.animationProgress.airMomentum, -maxMomentum, maxMomentum);
            
            if (control.animationProgress.airMomentum > 0)
            {
                control.FaceForward(true);
            }
            else if (control.animationProgress.airMomentum < 0)
            {
                control.FaceForward(false);
            }

            if (!IsBlock(control, speed))
                control.MoveForward(speed, Mathf.Abs(control.animationProgress.airMomentum));
        }

        private void ConstentMove(CharacterControl control, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (!IsBlock(control, speed))
            {
                control.MoveForward(speed, speedGraph.Evaluate(stateInfo.normalizedTime));
            }
        }

        private void ControlledMove(CharacterControl control, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (control.moveLeft && control.moveRight)
            {
                animator.SetBool(TransitionParameter.Move.ToString(), false);
                return;
            }

            if (!control.moveLeft && !control.moveRight)
            {
                animator.SetBool(TransitionParameter.Move.ToString(), false);
                return;
            }

            CheckTurn(control);

            if (control.moveLeft)
            {
                if (!IsBlock(control, speed))
                {
                    control.MoveForward(speed, speedGraph.Evaluate(stateInfo.normalizedTime));
                }
            }
            if (control.moveRight)
            {
                if (!IsBlock(control, speed))
                {
                    control.MoveForward(speed, speedGraph.Evaluate(stateInfo.normalizedTime));
                }
            }
        }

        void CheckTurn(CharacterControl control)
        {
            if (!lockDirection)
            {
                if (control.moveLeft)
                {
                    control.transform.rotation = Quaternion.Euler(0, 180, 0);
                }
                if (control.moveRight)
                {
                    control.transform.rotation = Quaternion.Euler(0, 0, 0);
                }
            }
        }

        bool IgnoringCharacterBox(Collider col)
        {
            if (!ignoreCharacterBox)
            {
                return false;
            }

            if (col.gameObject.GetComponent<CharacterControl>() != null)
            {
                return true;
            }

            return false;
        }

        bool IsBlock(CharacterControl control, float speed)
        {
            if (speed > 0)
            {
                sphereList = control.collisionSpheres.frontSpheres;
                dirBlock = 0.3f;
            }
            else
            {
                sphereList = control.collisionSpheres.backSpheres;
                dirBlock = -0.3f;
            }

            foreach (var o in sphereList)
            {
                Debug.DrawRay(o.transform.position, control.transform.forward * dirBlock, Color.yellow);
                RaycastHit hit;
                if (Physics.Raycast(o.transform.position, control.transform.forward * dirBlock, out hit, blockDistance))
                {
                    if (!control.ragdollParts.Contains(hit.collider))
                    {
                        if (!IsBodyPart(hit.collider)
                            && !Ledge.IsLedge(hit.collider.gameObject)
                            && !Ledge.IsLedgeChecker(hit.collider.gameObject)
                            && !IgnoringCharacterBox(hit.collider)
                            )
                        {
                            control.animationProgress.blockingObj = hit.collider.transform.root.gameObject;
                            return true;
                        }
                    }
                }
            }

            control.animationProgress.blockingObj = null;

            return false;
        }

        bool IsBodyPart(Collider col)
        {
            CharacterControl control = col.transform.root.GetComponent<CharacterControl>();
            if (control == null)
                return false;

            if (control.gameObject == col.gameObject)
                return false;

            if (control.ragdollParts.Contains(col))
                return true;

            return false;
        }
    }
}
