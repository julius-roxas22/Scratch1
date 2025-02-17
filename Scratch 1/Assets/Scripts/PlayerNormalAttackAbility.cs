using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DumbAssStudio
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "Create Ability/DumbAssStudio/PlayerNormalAttackAbility")]
    public class PlayerNormalAttackAbility : AbilityStateBase
    {
        private Vector3 currentAttackPos;
        private Quaternion currentAttackRotate;
        private List<AttackInfo> finishedAttacks = new List<AttackInfo>();

        public float startTimeAttack;
        public float endTimeAttack;

        public override void OnEnterAbility(PlayerController player, AnimatorStateInfo stateInfo, Animator animator)
        {
            currentAttackPos = player.transform.position;
            currentAttackRotate = player.transform.rotation;

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
            player.transform.position = currentAttackPos;
            player.transform.rotation = currentAttackRotate;

            if (!player.isAttacking)
            {
                animator.SetBool(TransitionParameters.Normal_Attack.ToString(), false);
            }

            registeredAttack(player, stateInfo);
            deRegisteredAttack(stateInfo);
        }

        public override void OnExitAbility(PlayerController player, AnimatorStateInfo stateInfo, Animator animator)
        {
            player.getNavAgent.destination = currentAttackPos;
            player.transform.rotation = currentAttackRotate;
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

                    if (info.attackAbility == this && !info.isRegistered)
                    {
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