using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roundbeargames_tutorial
{
    [CreateAssetMenu(fileName = "New State(Attack)", menuName = "Roundbeargames/AbilityData/Attack")]
    public class Attack : StateData
    {
        public float startAttackTime;
        public float endAttackTime;
        public List<string> colliderNames = new List<string>();
        public bool mustCollider;
        public bool mustFaceAttacker;
        public float lethalRange;
        public int maxHits;
        //public List<RuntimeAnimatorController> deathAnimators = new List<RuntimeAnimatorController>();

        private List<AttackInfo> finishedAttacks = new List<AttackInfo>();

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool(TransitionParameter.Attack.ToString(), false);

            GameObject obj = PoolManager.instance.GetObject(PoolObjectType.ATTACKINFO);
            AttackInfo info = obj.GetComponent<AttackInfo>();
            obj.SetActive(true);
            info.ResetInfo(this, characterState.GetCharacterControl(animator));

            if (!AttackManager.instance.currentAttcks.Contains(info))
            {
                AttackManager.instance.currentAttcks.Add(info);
            }
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            RegisterAttack(characterState, animator, stateInfo);
            DeregisterAttack(characterState, animator, stateInfo);
        }

        public void RegisterAttack(CharacterState state, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (stateInfo.normalizedTime >= startAttackTime && stateInfo.normalizedTime < endAttackTime)
            {
                foreach (AttackInfo info in AttackManager.instance.currentAttcks)
                {
                    if (info == null)
                        continue;

                    if (!info.isRegisterd && info.attackAbility == this)
                    {
                        info.Register(this);
                    }
                }
            }
        }

        public void DeregisterAttack(CharacterState state, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (stateInfo.normalizedTime >= endAttackTime)
            {
                foreach (AttackInfo info in AttackManager.instance.currentAttcks)
                {
                    if (info == null)
                    {
                        continue;
                    }

                    if (info.attackAbility == this && !info.isFinished)
                    {
                        info.isFinished = true;
                        info.GetComponent<PoolObject>().TurnOff();
                    }
                }
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            ClearAttack();
        }

        public void ClearAttack()
        {
            finishedAttacks.Clear();

            foreach(AttackInfo info in AttackManager.instance.currentAttcks)
            {
                finishedAttacks.Add(info);
            }

            foreach (AttackInfo info in finishedAttacks)
            {
                if (AttackManager.instance.currentAttcks.Contains(info))
                    AttackManager.instance.currentAttcks.Remove(info);
            }
        }

        //public RuntimeAnimatorController GetDeathAnimator()
        //{
        //    int index = Random.Range(0, deathAnimators.Count);
        //    return deathAnimators[index];
        //}
    }
}
