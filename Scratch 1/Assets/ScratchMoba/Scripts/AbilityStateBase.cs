using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DumbAssStudio
{
    public abstract class AbilityStateBase : ScriptableObject
    {
        public abstract void OnEnterAbility(PlayerController player, AnimatorStateInfo stateInfo, Animator animator);
        public abstract void OnUpdateAbility(PlayerController player, AnimatorStateInfo stateInfo, Animator animator);
        public abstract void OnExitAbility(PlayerController player, AnimatorStateInfo stateInfo, Animator animator);
    }
}

