using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DumbAssStudio
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "Create Ability/DumbAssStudio/PlayerForceTransitionAbility")]
    public class PlayerForceTransitionAbility : AbilityStateBase
    {

        [Range(0f, 1f)]
        public float transitionTiming;

        public override void OnEnterAbility(PlayerController player, AnimatorStateInfo stateInfo, Animator animator)
        {

        }

        public override void OnUpdateAbility(PlayerController player, AnimatorStateInfo stateInfo, Animator animator)
        {
            if (stateInfo.normalizedTime >= transitionTiming)
            {
                animator.SetBool(TransitionParameters.ForceTransition.ToString(), true);
            }
        }

        public override void OnExitAbility(PlayerController player, AnimatorStateInfo stateInfo, Animator animator)
        {
            animator.SetBool(TransitionParameters.ForceTransition.ToString(), false);
        }
    }
}