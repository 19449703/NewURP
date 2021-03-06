﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roundbeargames
{
    public enum AttackPartType
    {
        LEFT_HAND,
        RIGHT_HAND,
        LEFT_FOOT,
        RIGHT_FOOT,
    }

    [CreateAssetMenu(fileName = "Attack", menuName = "Roundbeargames/AbilityData/Attack")]
    public class Attack : StateData
    {
        public bool debug;
        public float startAttackTime;
        public float endAttackTime;
        public List<AttackPartType> attackParts = new List<AttackPartType>();
        public DeathType deathType;
        public bool mustCollider;
        public bool mustFaceAttacker;
        public float lethalRange;
        public int maxHits;

        [Header("Combo")]
        public float comboStartTime;
        public float comboEndTime;

        private List<AttackInfo> finishedAttacks = new List<AttackInfo>();

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool(TransitionParameter.Attack.ToString(), false);

            GameObject obj = PoolManager.instance.GetObject(PoolObjectType.ATTACKINFO);
            AttackInfo info = obj.GetComponent<AttackInfo>();
            obj.SetActive(true);
            info.ResetInfo(this, characterState.characterControl);

            if (!AttackManager.instance.currentAttcks.Contains(info))
            {
                AttackManager.instance.currentAttcks.Add(info);
            }
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            RegisterAttack(characterState, animator, stateInfo);
            DeregisterAttack(characterState, animator, stateInfo);
            CheckCombo(characterState, animator, stateInfo);
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
                        if (debug)
                        {
                            Debug.Log(this.name + " registed " + stateInfo.normalizedTime);
                        }
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

                        if (debug)
                        {
                            Debug.Log(this.name + " registed " + stateInfo.normalizedTime);
                        }
                    }
                }
            }
        }

        public void CheckCombo(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (stateInfo.normalizedTime >= comboStartTime)
            {
                if (stateInfo.normalizedTime < comboEndTime)
                {
                    if (characterState.characterControl.attack)
                    {
                        Debug.Log("uppercut triggered");
                        animator.SetBool(TransitionParameter.Attack.ToString(), true);
                    }
                }
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool(TransitionParameter.Attack.ToString(), false);
            ClearAttack();
        }

        public void ClearAttack()
        {
            finishedAttacks.Clear();

            foreach(AttackInfo info in AttackManager.instance.currentAttcks)
            {
                if (info == null || info.attackAbility == this)
                    finishedAttacks.Add(info);
            }

            foreach (AttackInfo info in finishedAttacks)
            {
                if (AttackManager.instance.currentAttcks.Contains(info))
                    AttackManager.instance.currentAttcks.Remove(info);
            }
        }
    }
}
