using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DumbAssStudio
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "Create Data/DumbAssStudio/HitReactionSwitchAnimator")]
    public class HitReactionSwitchAnimator : AbilityStateBase
    {
        public float switchTiming;

        [SerializeField] private RuntimeAnimatorController defaultAnimator;
        public override void OnEnterAbility(PlayerController player, AnimatorStateInfo stateInfo, Animator animator)
        {

        }

        public override void OnUpdateAbility(PlayerController player, AnimatorStateInfo stateInfo, Animator animator)
        {
            if (stateInfo.normalizedTime >= switchTiming)
            {
                player.getSkinnedMesh.runtimeAnimatorController = defaultAnimator;
            }
        }

        public override void OnExitAbility(PlayerController player, AnimatorStateInfo stateInfo, Animator animator)
        {

        }
    }
}
