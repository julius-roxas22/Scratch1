using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DumbAssStudio
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "Create Data/DumbAssStudio/Ability/PlayerResetAttack")]
    public class PlayerResetAttack : AbilityStateBase
    {
        public bool resetOnStart;
        public bool resetOnUpdate;
        public bool resetOnExit;

        public override void OnEnterAbility(PlayerController player, AnimatorStateInfo stateInfo, Animator animator)
        {
            if (resetOnStart)
            {
                if (player.getRandomAttack() != 0)
                {
                    player.setRandomAttack(0);
                    animator.SetBool(TransitionParameters.Normal_Attack1.ToString(), false);
                    animator.SetBool(TransitionParameters.Normal_Attack2.ToString(), false);
                    animator.SetBool(TransitionParameters.Normal_Attack3.ToString(), false);
                }
            }
        }

        public override void OnUpdateAbility(PlayerController player, AnimatorStateInfo stateInfo, Animator animator)
        {
            if (resetOnUpdate)
            {
                if (player.getRandomAttack() != 0)
                {
                    player.setRandomAttack(0);
                    animator.SetBool(TransitionParameters.Normal_Attack1.ToString(), false);
                    animator.SetBool(TransitionParameters.Normal_Attack2.ToString(), false);
                    animator.SetBool(TransitionParameters.Normal_Attack3.ToString(), false);
                }
            }
        }

        public override void OnExitAbility(PlayerController player, AnimatorStateInfo stateInfo, Animator animator)
        {
            if (resetOnExit)
            {
                if (player.getRandomAttack() != 0)
                {
                    player.setRandomAttack(0);
                    animator.SetBool(TransitionParameters.Normal_Attack1.ToString(), false);
                    animator.SetBool(TransitionParameters.Normal_Attack2.ToString(), false);
                    animator.SetBool(TransitionParameters.Normal_Attack3.ToString(), false);
                }
            }
        }
    }
}
