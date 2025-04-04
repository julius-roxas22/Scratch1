using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DumbAssStudio
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "Create Data/DumbAssStudio/Ability/PlayerIdle")]
    public class PlayerIdleAbility : AbilityStateBase
    {
        public override void OnEnterAbility(PlayerController player, AnimatorStateInfo stateInfo, Animator animator)
        {

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
                animator.SetBool(TransitionParameters.Walk.ToString(), true);
                player.getNavAgent.isStopped = false;
            }

            if (player.isAttacking)
            {
                player.setRandomAttack(Random.Range(1, 4));

                if (player.getRandomAttack() == 1)
                {
                    animator.SetBool(TransitionParameters.Normal_Attack1.ToString(), true);
                }
                else if (player.getRandomAttack() == 2)
                {
                    animator.SetBool(TransitionParameters.Normal_Attack2.ToString(), true);
                }
                else if (player.getRandomAttack() == 3)
                {
                    animator.SetBool(TransitionParameters.Normal_Attack3.ToString(), true);
                }
            }
        }
    }
}
