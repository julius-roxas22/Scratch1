using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DumbAssStudio
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "Create Ability/DumbAssStudio/PlayerIdle")]
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
            if (player.getPlayerAnimatorProgress.IsWalking)
            {
                player.getNavMeshAgent.isStopped = false;
                animator.SetBool(TransitionParameters.Walk.ToString(), true);
            }
        }
    }
}
