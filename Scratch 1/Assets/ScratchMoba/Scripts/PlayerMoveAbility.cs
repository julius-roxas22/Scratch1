using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DumbAssStudio
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "Create Data/DumbAssStudio/Ability/PlayerMove")]
    public class PlayerMoveAbility : AbilityStateBase
    {
        public float stoppingDistance;

        public override void OnEnterAbility(PlayerController player, AnimatorStateInfo stateInfo, Animator animator)
        {
            if (player.getRandomAttack() != 0)
            {
                player.setRandomAttack(0);
            }
            animator.SetBool(TransitionParameters.Normal_Attack1.ToString(), false);
            animator.SetBool(TransitionParameters.Normal_Attack2.ToString(), false);
            animator.SetBool(TransitionParameters.Normal_Attack3.ToString(), false);
        }

        public override void OnUpdateAbility(PlayerController player, AnimatorStateInfo stateInfo, Animator animator)
        {
            controlledMoved(player, animator);
        }

        public override void OnExitAbility(PlayerController player, AnimatorStateInfo stateInfo, Animator animator)
        {

        }

        private void controlledMoved(PlayerController player, Animator animator)
        {
            if (player.isWalking)
            {
                player.moveTowardsTo(player.getTargetHitPoint);
            }

            if (!player.isWalking)
            {
                animator.SetBool(TransitionParameters.Walk.ToString(), false);
                player.getNavAgent.isStopped = true;
            }

            float stoppingPointDist = (player.getTargetHitPoint - player.transform.position).sqrMagnitude;

            if (stoppingPointDist < stoppingDistance)
            {
                VirtualInpuManager.getInstance.isWalking = false;
            }
        }
    }
}

