﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public enum TransitionConditionType
    {
        UP,
        DOWN,
        LEFT,
        RIGHT,
        ATTACK,
        JUMP,
        GRABBING_LEDGE,

        LEFT_OR_RIGHT,

        GROUNDED,
        MOVE_FORWARD,
    }

    [CreateAssetMenu(fileName = "TransitionIndexer", menuName = "Roundbeargames/AbilityData/TransitionIndexer")]
    public class TransitionIndexer : StateData
    {
        public int index;
        public List<TransitionConditionType> transitionConditions = new List<TransitionConditionType>();

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.characterControl;
            if (MakeTransition(control))
            {
                animator.SetInteger(TransitionParameter.TransitionIndex.ToString(), index);
            }
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (animator.GetInteger(TransitionParameter.TransitionIndex.ToString()) == 0)
            {
                if (MakeTransition(characterState.characterControl))
                {
                    animator.SetInteger(TransitionParameter.TransitionIndex.ToString(), index);
                }
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetInteger(TransitionParameter.TransitionIndex.ToString(), 0);
        }

        private bool MakeTransition(CharacterControl control)
        {
            foreach(var c in transitionConditions)
            {
                switch(c)
                {
                    case TransitionConditionType.UP:
                        {
                            if (!control.moveUp)
                            {
                                return false;
                            }
                        }
                        break;

                    case TransitionConditionType.DOWN:
                        {
                            if (!control.moveDown)
                            {
                                return false;
                            }
                        }
                        break;

                    case TransitionConditionType.LEFT:
                        {
                            if (!control.moveLeft)
                            {
                                return false;
                            }
                        }
                        break;

                    case TransitionConditionType.RIGHT:
                        {
                            if (!control.moveRight)
                            {
                                return false;
                            }
                        }
                        break;

                    case TransitionConditionType.ATTACK:
                        {
                            if (!control.attack)
                            {
                                return false;
                            }
                        }
                        break;

                    case TransitionConditionType.JUMP:
                        {
                            if (!control.jump)
                            {
                                return false;
                            }
                        }
                        break;

                    case TransitionConditionType.GRABBING_LEDGE:
                        {
                            if (!control.ledgeChecker.isGrabbingLedge)
                            {
                                return false;
                            }
                        }
                        break;

                    case TransitionConditionType.LEFT_OR_RIGHT:
                        {
                            if (!control.moveLeft && !control.moveRight)
                            {
                                return false;
                            }
                            break;
                        }

                    case TransitionConditionType.GROUNDED:
                        {
                            if (!control.skinedMeshAnimator.GetBool(TransitionParameter.Grounded.ToString()))
                            {
                                return false;
                            }
                            break;
                        }

                    case TransitionConditionType.MOVE_FORWARD:
                        {
                            if (control.IsFaceingForward())
                            {
                                if (!control.moveRight)
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                if (!control.moveLeft)
                                {
                                    return false;
                                }
                            }
                            break;
                        }
                }
            }

            return true;
        }
    }
}
