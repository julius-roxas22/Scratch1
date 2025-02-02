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

            ControlledMoved(player, animator);
        }

        public override void OnExitAbility(PlayerController player, AnimatorStateInfo stateInfo, Animator animator)
        {

        }
        private void ControlledMoved(PlayerController player, Animator animator)
        {
            if (player.GetPlayerAnimatorProgress.IsWalking)
            {
                player.GetNavMeshAgent.isStopped = false;
                animator.SetBool(TransitionParameters.Walk.ToString(), true);
            }
        }
    }
}
