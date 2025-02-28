using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DumbAssStudio
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "Create Data/DumbAssStudio/NPC_Ability/NpcAttack")]
    public class NpcAttack : AbilityStateBase
    {
        private PlayerController targetPlayer;

        public override void OnEnterAbility(PlayerController player, AnimatorStateInfo stateInfo, Animator animator)
        {
            if (null == targetPlayer)
            {
                targetPlayer = player.getNpcProgress.getPlayableController();
            }

            player.forwardLook = true;
            player.isWalking = false;
            player.isAttacking = true;
        }

        public override void OnUpdateAbility(PlayerController player, AnimatorStateInfo stateInfo, Animator animator)
        {
            float dist = (targetPlayer.transform.position - player.transform.position).sqrMagnitude;

            if (dist > player.getDefense.attackRange)
            {
                player.getNpcProgress.getNpcTransitionSubset(NpcTransitionType.Walk).gameObject.SetActive(true);
            }
            else
            {
                player.getNpcProgress.getNpcTransitionSubset(NpcTransitionType.Attack).gameObject.SetActive(false);
            }
        }

        public override void OnExitAbility(PlayerController player, AnimatorStateInfo stateInfo, Animator animator)
        {

        }


    }
}


