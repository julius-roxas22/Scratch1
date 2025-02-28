using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DumbAssStudio
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "Create Data/DumbAssStudio/NPC_Ability/NpcWalk")]
    public class NpcWalk : AbilityStateBase
    {
        public override void OnEnterAbility(PlayerController player, AnimatorStateInfo stateInfo, Animator animator)
        {
            player.getTargetHitPoint = GameManager.getInstance.getPlayableCharacter("Player").transform.position;
            player.isWalking = true;
            Debug.Log(player.name + " walking towards to " + GameManager.getInstance.getPlayableCharacter("Player").name);
        }

        public override void OnUpdateAbility(PlayerController player, AnimatorStateInfo stateInfo, Animator animator)
        {

        }

        public override void OnExitAbility(PlayerController player, AnimatorStateInfo stateInfo, Animator animator)
        {

        }


    }
}


