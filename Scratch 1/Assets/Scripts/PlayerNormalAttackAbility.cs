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
        public float endTimeAttack;

        public override void OnEnterAbility(PlayerController player, AnimatorStateInfo stateInfo, Animator animator)
        {
            currentAttackPos = player.transform.position;
            currentAttackRotate = player.transform.rotation;
        }

        public override void OnUpdateAbility(PlayerController player, AnimatorStateInfo stateInfo, Animator animator)
        {
            player.transform.position = currentAttackPos;
            player.transform.rotation = currentAttackRotate;

            if (!player.isAttacking)
            {
                animator.SetBool(TransitionParameters.Normal_Attack.ToString(), false);
            }
        }

        public override void OnExitAbility(PlayerController player, AnimatorStateInfo stateInfo, Animator animator)
        {
            player.getNavAgent.destination = currentAttackPos;
            player.transform.rotation = currentAttackRotate;
            player.getDamageDetector.takeDamage(Random.Range(1, 5), player);
        }
    }
}