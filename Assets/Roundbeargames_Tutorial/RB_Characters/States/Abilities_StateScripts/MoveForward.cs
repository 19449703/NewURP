﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames_tutorial
{
    [CreateAssetMenu(fileName = "MoveForward", menuName = "Roundbeargames/AbilityData/MoveForward")]
    public class MoveForward : StateData
    {
        public bool allowEarlyTurn;
        public bool lockDirection;
        public AnimationCurve speedGraph;
        public float speed;
        public float blockDistance;
        public bool constant;

        [Header("Momentum")]
        public bool useMomentum;
        public float maxMomentum;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);

            if (allowEarlyTurn && !control.animationProgress.disallowEarlyTurn)
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

            control.animationProgress.disallowEarlyTurn = false;
            control.animationProgress.airMomentum = 0;
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);

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
            CharacterControl control = characterState.GetCharacterControl(animator);

            control.animationProgress.airMomentum = 0;
        }

        public void UpdateMomentum(CharacterControl control, AnimatorStateInfo stateInfo)
        {
            if (control.moveLeft)
            {
                control.animationProgress.airMomentum -= speedGraph.Evaluate(stateInfo.normalizedTime) * Time.deltaTime;
            }

            if (control.moveRight)
            {
                control.animationProgress.airMomentum += speedGraph.Evaluate(stateInfo.normalizedTime) * Time.deltaTime;
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

            if (!CheckFront(control))
                control.MoveForward(speed, Mathf.Abs(control.animationProgress.airMomentum));
        }

        private void ConstentMove(CharacterControl control, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (!CheckFront(control))
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
                if (!CheckFront(control))
                {
                    control.MoveForward(speed, speedGraph.Evaluate(stateInfo.normalizedTime));
                }
            }
            if (control.moveRight)
            {
                if (!CheckFront(control))
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
            
        bool CheckFront(CharacterControl control)
        {
            foreach (var o in control.frontSpheres)
            {
                Debug.DrawRay(o.transform.position, control.transform.forward * 0.3f, Color.yellow);
                RaycastHit hit;
                if (Physics.Raycast(o.transform.position, control.transform.forward, out hit, blockDistance))
                {
                    if (!control.ragdollParts.Contains(hit.collider))
                    {
                        if (!IsBodyPart(hit.collider)
                            && !Ledge.IsLedge(hit.collider.gameObject)
                            && !Ledge.IsLedgeChecker(hit.collider.gameObject)
                            )
                        {
                            return true;
                        }
                    }
                }
            }

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
