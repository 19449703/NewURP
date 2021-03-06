﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public class DamageDecetor : MonoBehaviour
    {
        CharacterControl control;
        GeneralBodyPart damagedPart;
        public int damageTaken = 0;

        private void Awake()
        {
            control = GetComponent<CharacterControl>();
        }

        private void Update()
        {
            if (AttackManager.instance.currentAttcks.Count > 0)
            {
                CheckAttack();
            }
        }

        private void CheckAttack()
        {
            foreach(var info in AttackManager.instance.currentAttcks)
            {
                if (info == null
                    || !info.isRegisterd
                    || info.isFinished
                    || info.currentHits >= info.maxHits
                    || info.attacker == control
                    )
                {
                    continue;
                }

                if (info.mustFaceAttacker)
                {
                    Vector3 vec = this.transform.position - info.attacker.transform.position;
                    if (vec.z * info.attacker.transform.forward.z < 0)
                    {
                        continue;
                    }
                }

                if (info.mustCollider)
                {
                    if (IsCollided(info))
                    {
                        TakeDamage(info);
                    }
                }
                else
                {
                    float dist = Vector3.SqrMagnitude(this.gameObject.transform.position - info.attacker.transform.position);
                    if (dist <= info.lethalRange)
                    {
                        TakeDamage(info);
                    }
                }
            }
        }

        private bool IsCollided(AttackInfo info)
        {
            foreach (TriggerDetector trigger in control.GetAllTriggers())
            {
                foreach (Collider collider in trigger.collidingParts)
                {
                    foreach (AttackPartType part in info.attackParts)
                    {
                        if (part == AttackPartType.LEFT_HAND)
                        {
                            if (collider.gameObject == info.attacker.leftHand_Attack)
                            {
                                damagedPart = trigger.generalBodyPart;
                                return true;
                            }
                        }
                        else if (part == AttackPartType.RIGHT_HAND)
                        {
                            if (collider.gameObject == info.attacker.rightHand_Attack)
                            {
                                damagedPart = trigger.generalBodyPart;
                                return true;
                            }
                        }
                        else if (part == AttackPartType.LEFT_FOOT)
                        {
                            if (collider.gameObject == info.attacker.leftFoot_Attack)
                            {
                                damagedPart = trigger.generalBodyPart;
                                return true;
                            }
                        }
                        else if (part == AttackPartType.RIGHT_FOOT)
                        {
                            if (collider.gameObject == info.attacker.rightFoot_Attack)
                            {
                                damagedPart = trigger.generalBodyPart;
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        private void TakeDamage(AttackInfo info)
        {
            if (damageTaken > 0)
                return;

            if (info.mustCollider)
                CameraManager.instance.ShakeCamera(0.35f);

            Debug.Log(info.attacker.gameObject.name + " hits：" + this.gameObject.name);
            Debug.Log(this.gameObject.name + " hit " + damagedPart.ToString());

			control.skinedMeshAnimator.runtimeAnimatorController = DeathAnimationManager.instance.GetAnimator(damagedPart, info);
            //control.CacheCharacterControl(control.skinedMeshAnimator);
            info.currentHits++;

            control.GetComponent<BoxCollider>().enabled = false;
			control.ledgeChecker.GetComponent<BoxCollider>().enabled = false;
            control.RIGID_BODY.useGravity = false;
            control.navMeshObstacle.carving = false;

            damageTaken++;
        }
    }
}
