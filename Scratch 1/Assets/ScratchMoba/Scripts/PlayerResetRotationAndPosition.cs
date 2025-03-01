using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DumbAssStudio
{


    [CreateAssetMenu(fileName = "New Ability", menuName = "Create Data/DumbAssStudio/Ability/PlayerResetRotationAndPosition")]
    public class PlayerResetRotationAndPosition : AbilityStateBase
    {
        [Header("Position")]
        public bool onEnableResetPosition;
        public bool onUpdateResetPosition;
        public bool onExitResetPosition;

        [Header("Rotation")]
        public bool onEnableResetRotation;
        public bool onUpdateResetRotation;
        public bool onExitResetRotation;

        private Vector3 currentAttackPos;
        private Quaternion currentAttackRotate;

        public override void OnEnterAbility(PlayerController player, AnimatorStateInfo stateInfo, Animator animator)
        {
            currentAttackPos = player.transform.position;
            currentAttackRotate = player.transform.rotation;
        }

        public override void OnUpdateAbility(PlayerController player, AnimatorStateInfo stateInfo, Animator animator)
        {
            if (onEnableResetPosition)
            {
                if (onUpdateResetPosition)
                {
                    player.transform.position = currentAttackPos;
                }
            }

            if (onEnableResetRotation)
            {
                if (onUpdateResetRotation)
                {
                    player.transform.rotation = currentAttackRotate;
                }
            }
        }

        public override void OnExitAbility(PlayerController player, AnimatorStateInfo stateInfo, Animator animator)
        {
            if (onEnableResetPosition)
            {
                if (onExitResetPosition)
                {
                    player.transform.position = currentAttackPos;
                }
            }

            if (onEnableResetRotation)
            {
                if (onExitResetRotation)
                {
                    player.transform.rotation = currentAttackRotate;
                }
            }
        }

    }
}