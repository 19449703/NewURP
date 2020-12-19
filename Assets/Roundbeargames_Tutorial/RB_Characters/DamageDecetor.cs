using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames_tutorial
{
    public class DamageDecetor : MonoBehaviour
    {
        CharacterControl control;
        GeneralBodyPart damagedPart;

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

                if (info.mustCollider)
                {
                    if (IsCollided(info))
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
                    foreach (string name in info.colliderNames)
                    {
                        if (name == collider.gameObject.name)
                        {
                            damagedPart = trigger.generalBodyPart;
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private void TakeDamage(AttackInfo info)
        {
            CameraManager.instance.ShakeCamera(0.35f);

            Debug.Log(info.attacker.gameObject.name + " hits：" + this.gameObject.name);
            Debug.Log(this.gameObject.name + " hit " + damagedPart.ToString());

			//control.skinedMeshAnimator.runtimeAnimatorController = info.attackAbility.GetDeathAnimator();
			control.skinedMeshAnimator.runtimeAnimatorController = DeathAnimationManager.instance.GetAnimator(damagedPart, info);
			info.currentHits++;

            control.GetComponent<BoxCollider>().enabled = false;
            control.RIGID_BODY.useGravity = false;
        }
    }
}
