using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DumbAssStudio
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "Create Ability/DumbAssStudio/PlayerMove")]
    public class PlayerMoveAbility : AbilityStateBase
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
                player.moveTowardsTo(player.getTargetHitPoint);
            }

            if (!player.isWalking)
            {
                animator.SetBool(TransitionParameters.Walk.ToString(), false);
                player.getNavAgent.isStopped = true;
            }
        }
    }
}

