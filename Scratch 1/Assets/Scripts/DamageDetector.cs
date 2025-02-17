using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DumbAssStudio
{
    public class DamageDetector : MonoBehaviour
    {
        private PlayerController playerController;

        private int currentHealth;

        private void Awake()
        {
            playerController = GetComponent<PlayerController>();
            currentHealth = playerController.getDefense.currentHealth;
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

                if (null != info.attacker.interactionObject)
                {
                    takeDamage(info);
                }
            }
        }

        private void takeDamage(AttackInfo info)
        {
            GameObject enemy = info.attacker.interactionObject.gameObject;

            GameObjectType objType = enemy.GetComponent<GameObjectType>();

            currentHealth = playerController.getDefense.currentHealth;

            if (objType.objectType == ObjectType.Enemy)
            {
                currentHealth -= Random.Range(1, 5);
                playerController.getDefense.currentHealth = currentHealth;
            }

            if (currentHealth <= 0)
            {
                Debug.Log(playerController.name + " is dead");
                info.attacker.isAttacking = false;
            }
        }
    }
}

