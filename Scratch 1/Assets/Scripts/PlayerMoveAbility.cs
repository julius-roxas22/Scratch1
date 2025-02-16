using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DumbAssStudio
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "Create Ability/DumbAssStudio/PlayerMove")]
    public class PlayerMoveAbility : AbilityStateBase
    {
        public float movementSpeed;
        public float stoppingDistance;

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
            float dist = (player.getRayCastHitPoint - player.transform.position).sqrMagnitude;

            if (player.isMoving)
            {
                if (dist < stoppingDistance && isTheSamePlayer(player))
                {
                    VirtualInpuManager.getInstance.isMoving = false;
                }
                player.playerMove(movementSpeed);
            }

            if (!player.isMoving)
            {
                animator.SetBool(TransitionParameters.Walk.ToString(), false);
                player.getNavMeshAgent.isStopped = true;
            }
        }

        bool isTheSamePlayer(PlayerController playerController)
        {

            foreach (PlayerController p in GameManager.getInstance.playerList)
            {
                if (!p.Equals(playerController))
                {
                    return false;
                }
            }

            return true;
        }
    }
}

