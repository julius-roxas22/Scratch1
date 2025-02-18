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

                if (info.mustCollide)
                {
                    foreach (GameObject obj in info.attacker.objectCollidingParts)
                    {
                        foreach (TriggerDetector t in playerController.getAllTriggers())
                        {
                            foreach(Collider col in t.collidingParts)
                            {

                            }

                            if (obj.name.Equals(t.gameObject.name))
                            {
                                takeDamage(info, t.name);
                            }
                        }
                    }
                }
            }
        }

        private void takeDamage(AttackInfo info, string objName)
        {
            GameObjectType enemyObjType = playerController.GetComponent<GameObjectType>();
            GameObjectType attackerObjType = info.attacker.GetComponent<GameObjectType>();

            if (enemyObjType.objectType == ObjectType.Enemy && enemyObjType.objectType != attackerObjType.objectType)
            {
                Debug.Log(playerController.name + " hit into " + objName + " part");
            }
        }
    }
}

