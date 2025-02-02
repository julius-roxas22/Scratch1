using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DumbAssStudio
{
    public class StateBase : StateMachineBehaviour
    {
        private PlayerController player;
        public List<AbilityStateBase> Abilities = new List<AbilityStateBase>();
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            foreach (AbilityStateBase ability in Abilities)
            {
                ability.OnEnterAbility(GetPlayer(animator), stateInfo, animator);
            }
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            foreach (AbilityStateBase ability in Abilities)
            {
                ability.OnUpdateAbility(GetPlayer(animator), stateInfo, animator);
            }
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            foreach (AbilityStateBase ability in Abilities)
            {
                ability.OnExitAbility(GetPlayer(animator), stateInfo, animator);
            }
        }

        public PlayerController GetPlayer(Animator animator)
        {
            if (null == player)
            {
                player = animator.transform.root.GetComponentInChildren<PlayerController>();
            }
            return player;
        }
    }
}