using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DumbAssStudio
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "Create Data/DumbAssStudio/NPC_Ability/NpcWalk")]
    public class NpcWalk : AbilityStateBase
    {
        private PlayerController targetPlayer;
        public float stoppingDist;

        public override void OnEnterAbility(PlayerController player, AnimatorStateInfo stateInfo, Animator animator)
        {
            if (null == targetPlayer)
            {
                targetPlayer = player.getNpcProgress.getPlayableController();
            }

            //player.getTargetHitPoint = targetPlayer.transform.position; // temporary solution
            //player.isWalking = true; // temporary solution
        }

        public override void OnUpdateAbility(PlayerController player, AnimatorStateInfo stateInfo, Animator animator)
        {
            float dist = (targetPlayer.transform.position - player.transform.position).sqrMagnitude;

            player.getTargetHitPoint = targetPlayer.transform.position;

            if (dist < stoppingDist && !player.getManualInput.enabled)
            {
                player.interactionObject = targetPlayer.gameObject;
                player.isWalking = false;
                player.isAttacking = true;
                player.forwardLook = true;
            }
            else
            {
                player.isWalking = true;
                player.isAttacking = false;
                player.forwardLook = true;
                player.interactionObject = null;
            }
        }

        public override void OnExitAbility(PlayerController player, AnimatorStateInfo stateInfo, Animator animator)
        {

        }


    }
}


