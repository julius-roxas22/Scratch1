using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DumbAssStudio
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "Create Ability/DumbAssStudio/PlayerMove")]
    public class PlayerMoveAbility : AbilityStateBase
    {
        public float stoppingDistance;

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
                player.GetNavMeshAgent.SetDestination(player.GetRayCastHitPoint);
            }

            float distance = (player.GetRayCastHitPoint - player.transform.position).sqrMagnitude; /* Vector3.Distance(player.transform.position, player.GetRayCastHitPoint) */;

            if (distance < stoppingDistance)
            {
                player.GetPlayerAnimatorProgress.IsWalking = false;
            }

            if (VirtualInpuManager.GetInstance.IsStop)
            {
                player.GetNavMeshAgent.isStopped = true;
                player.GetPlayerAnimatorProgress.IsWalking = false;
            }

            if (!player.GetPlayerAnimatorProgress.IsWalking)
            {
                animator.SetBool(TransitionParameters.Walk.ToString(), false);
            }
        }
    }
}

