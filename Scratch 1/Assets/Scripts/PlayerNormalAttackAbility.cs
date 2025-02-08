using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DumbAssStudio
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "Create Ability/DumbAssStudio/PlayerNormalAttackAbility")]
    public class PlayerNormalAttackAbility : AbilityStateBase
    {

        public override void OnEnterAbility(PlayerController player, AnimatorStateInfo stateInfo, Animator animator)
        {

        }

        public override void OnUpdateAbility(PlayerController player, AnimatorStateInfo stateInfo, Animator animator)
        {
            player.transform.position = player.offSetToAttack;
            player.canlookRotate = false;
        }

        public override void OnExitAbility(PlayerController player, AnimatorStateInfo stateInfo, Animator animator)
        {
            player.canlookRotate = true;
        }
    }
}