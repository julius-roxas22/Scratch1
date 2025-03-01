using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DumbAssStudio
{
    //public enum AttackType
    //{
    //    LEFT_HAND,
    //    RIGHT_HAND
    //}

    [CreateAssetMenu(fileName = "New Ability", menuName = "Create Data/DumbAssStudio/Ability/PlayerNormalAttackAbility")]
    public class PlayerNormalAttackAbility : AbilityStateBase
    {
        private List<AttackInfo> finishedAttacks = new List<AttackInfo>();

        //public List<AttackType> attackTypes = new List<AttackType>();
        public HitType hitType;
        public float startTimeAttack;
        public float endTimeAttack;

        public bool mustCollide;
        public bool onEnableDebug;

        public override void OnEnterAbility(PlayerController player, AnimatorStateInfo stateInfo, Animator animator)
        {
            GameObject obj = PoolManager.getInstance.InstantiatePoolObjectPrefab(PoolObjectType.ATTACKINFO);
            AttackInfo info = obj.GetComponent<AttackInfo>();

            info.gameObject.SetActive(true);
            info.resetAttackInfo(player, this);

            if (!AttackManager.getInstance.currentAttacks.Contains(info))
            {
                AttackManager.getInstance.currentAttacks.Add(info);
            }
        }

        public override void OnUpdateAbility(PlayerController player, AnimatorStateInfo stateInfo, Animator animator)
        {

            registeredAttack(player, stateInfo);
            deRegisteredAttack(stateInfo);
        }

        public override void OnExitAbility(PlayerController player, AnimatorStateInfo stateInfo, Animator animator)
        {
            clearAttacks();
        }

        private void registeredAttack(PlayerController playerController, AnimatorStateInfo stateInfo)
        {
            if (startTimeAttack <= stateInfo.normalizedTime && endTimeAttack > stateInfo.normalizedTime)
            {
                foreach (AttackInfo info in AttackManager.getInstance.currentAttacks)
                {
                    if (null == info)
                    {
                        continue;
                    }

                    if (this == info.attackAbility && !info.isRegistered)
                    {
                        if (onEnableDebug)
                        {
                            Debug.Log("Register attack in " + stateInfo.normalizedTime);
                        }

                        info.registeredAttack(playerController, this);
                    }
                }
            }
        }

        private void deRegisteredAttack(AnimatorStateInfo stateInfo)
        {
            if (stateInfo.normalizedTime >= endTimeAttack)
            {
                foreach (AttackInfo info in AttackManager.getInstance.currentAttacks)
                {
                    if (null == info)
                    {
                        continue;
                    }

                    if (this == info.attackAbility && !info.isFinished)
                    {
                        if (onEnableDebug)
                        {
                            Debug.Log("De-Register attack in " + stateInfo.normalizedTime);
                        }

                        info.isFinished = true;
                        info.GetComponent<PoolObject>().turnOff();
                    }
                }
            }
        }

        private void clearAttacks()
        {
            finishedAttacks.Clear();

            foreach (AttackInfo info in AttackManager.getInstance.currentAttacks)
            {
                if (null == info || this == info.attackAbility)
                {
                    finishedAttacks.Add(info);
                }
            }

            foreach (AttackInfo info in finishedAttacks)
            {
                if (AttackManager.getInstance.currentAttacks.Contains(info))
                {
                    AttackManager.getInstance.currentAttacks.Remove(info);
                }
            }
        }
    }
}