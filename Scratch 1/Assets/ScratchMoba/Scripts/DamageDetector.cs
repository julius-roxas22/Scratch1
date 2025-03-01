using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DumbAssStudio
{
    public class DamageDetector : MonoBehaviour
    {
        private PlayerController playerController;
        private eBodyParts bodyParts;

        //[SerializeField] private List<RuntimeAnimatorController> hitReactionList = new List<RuntimeAnimatorController>();

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
                    if (isCollidedPart(info))
                    {
                        takeDamage(info);
                    }
                }
            }
        }

        private bool isCollidedPart(AttackInfo info)
        {
            foreach (TriggerDetector t in playerController.getAllTriggers())
            {
                foreach (Collider col in t.collidingParts)
                {
                    if (info.attacker.attackCollidingParts.Contains(col.gameObject))
                    {
                        bodyParts = t.bodyParts;
                        return true;
                    }
                    //foreach (AttackType types in info.attackTypes)
                    //{
                    //    if (types == AttackType.RIGHT_HAND)
                    //    {
                    //        if (col.gameObject == info.attacker.rightHandAttack)
                    //        {
                    //            bodyParts = t.bodyParts;
                    //            return true;
                    //        }
                    //    }
                    //    else if (types == AttackType.LEFT_HAND)
                    //    {
                    //        if (col.gameObject == info.attacker.leftHandAttack)
                    //        {
                    //            bodyParts = t.bodyParts;
                    //            return true;
                    //        }
                    //    }
                    //}
                }
            }
            return false;
        }

        private void takeDamage(AttackInfo info)
        {
            //playerController.getSkinnedMesh.runtimeAnimatorController = HitReactionManager.getInstance.getHitAnimatorController(info.hitType);
            //playerController.getSkinnedMesh.runtimeAnimatorController = DeathManager.getInstance.getDeathType(bodyParts);
            //Debug.Log(playerController.name + " hit by " + info.attacker.name + " into " + bodyParts.ToString());
            Debug.Log(playerController.name + " hit by " + info.attacker.name);
        }
    }
}

