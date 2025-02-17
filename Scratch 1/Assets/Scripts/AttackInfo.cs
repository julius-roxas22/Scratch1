using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DumbAssStudio
{
    public class AttackInfo : MonoBehaviour
    {
        public PlayerController attacker;
        public PlayerNormalAttackAbility attackAbility;

        public float startTimeAttack;
        public float endTimeAttack;
        public float attackRange;

        public bool isRegistered;
        public bool isFinished;

        public void resetAttackInfo(PlayerController playerController, PlayerNormalAttackAbility attack)
        {
            isRegistered = false;
            isFinished = false;

            attackAbility = attack;
            attacker = playerController;
        }

        public void registeredAttack(PlayerController playerController, PlayerNormalAttackAbility attack)
        {
            isRegistered = true;
            attackAbility = attack;
            startTimeAttack = attack.startTimeAttack;
            endTimeAttack = attack.endTimeAttack;
            attackRange = playerController.getDefense.attackRange;
        }
    }
}


