using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DumbAssStudio
{
    public enum AttackType
    {
        LEFT_HAND,
        RIGHT_HAND
    }

    [CreateAssetMenu(fileName = "New Ability", menuName = "Create Ability/DumbAssStudio/PlayerNormalAttackAbility")]
    public class PlayerNormalAttackAbility : AbilityStateBase
    {
        private Vector3 currentAttackPos;
        private Quaternion currentAttackRotate;
        private List<AttackInfo> finishedAttacks = new List<AttackInfo>();

        public List<AttackType> attackTypes = new List<AttackType>();
        public float startTimeAttack;
        public float endTimeAttack;

        public bool mustCollide;
        public bool onEnableDebug;

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

            if (player.getRandomAttack() == 1)
            {
                animator.SetBool(TransitionParameters.Normal_Attack1.ToString(), false);
            }
            else if (player.getRandomAttack() == 2)
            {
                animator.SetBool(TransitionParameters.Normal_Attack2.ToString(), false);
            }

            registeredAttack(player, stateInfo);
            deRegisteredAttack(stateInfo);
        }

        public override void OnExitAbility(PlayerController player, AnimatorStateInfo stateInfo, Animator animator)
        {
            player.getNavAgent.destination = currentAttackPos;
            player.transform.rotation = currentAttackRotate;

            if (player.getRandomAttack() != 0)
            {
                player.setRandomAttack(0);
            }

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