using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DumbAssStudio
{
    public class DamageDetector : MonoBehaviour
    {
        private PlayerController playerController;

        private void Awake()
        {
            playerController = GetComponent<PlayerController>();
        }

        private void Update()
        {
            if (AttackManager.getInstance.currentAttacks.Count > 0)
            {
                checkDamage();
            }
        }

        private void checkDamage()
        {
            foreach (AttackInfo info in AttackManager.getInstance.currentAttacks)
            {
                if (null == info)
                {
                    continue;
                }

                if (!info.isRegistered)
                {
                    continue;
                }

                if (info.isFinished)
                {
                    continue;
                }

                if (info.attacker == playerController)
                {
                    continue;
                }

                float dist = (playerController.transform.position - info.attacker.transform.position).sqrMagnitude;

                if (dist < info.attackRange)
                {
                    takeDamage(info);
                }
            }
        }

        private void takeDamage(AttackInfo info)
        {
            GameObjectType enemyObjType = playerController.GetComponent<GameObjectType>();
            GameObjectType attackerObjType = info.attacker.GetComponent<GameObjectType>();

            if (enemyObjType.objectType == ObjectType.Enemy && enemyObjType.objectType != attackerObjType.objectType)
            {
                Debug.Log(playerController.name + " hit by " + info.attacker.name);
            }
        }
    }
}

