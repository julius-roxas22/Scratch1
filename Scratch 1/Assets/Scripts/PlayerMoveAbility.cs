using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DumbAssStudio
{
    [CreateAssetMenu(fileName = "New Ability", menuName = "Create Ability/DumbAssStudio/PlayerMove")]
    public class PlayerMoveAbility : AbilityStateBase
    {
        public float movementSpeed;
        public float stoppingDistance;

        public override void OnEnterAbility(PlayerController player, AnimatorStateInfo stateInfo, Animator animator)
        {

        }

        public override void OnUpdateAbility(PlayerController player, AnimatorStateInfo stateInfo, Animator animator)
        {
            controlledMoved(player, animator);
        }

        public override void OnExitAbility(PlayerController player, AnimatorStateInfo stateInfo, Animator animator)
        {

        }

        private void controlledMoved(PlayerController player, Animator animator)
        {
            float dist = (player.getRayCastHitPoint - player.transform.position).sqrMagnitude;

            if (player.isMoving)
            {
                if (dist < stoppingDistance)
                {
                    VirtualInpuManager.getInstance.isMoving = false;
                }
                else if (null == player.interactionObject)
                {
                    player.playerMove(movementSpeed, player.getRayCastHitPoint);
                }
                else if (null != player.interactionObject)
                {
                    GameObject enemy = player.interactionObject;
                    GameObjectType oType = enemy.GetComponent<GameObjectType>();
                    if (oType.objectType == ObjectType.Enemy)
                    {
                        player.playerMove(movementSpeed, enemy.transform.position);
                    }
                }
            }

            if (!player.isMoving)
            {
                GameObject obj = player.interactionObject;

                if (null == obj)
                {
                    animator.SetBool(TransitionParameters.Walk.ToString(), false);
                    player.getNavMeshAgent.isStopped = true;
                }
                else if (null != obj)
                {
                    GameObjectType oType = obj.GetComponent<GameObjectType>();
                    if (oType.objectType == ObjectType.Enemy)
                    {
                        animator.SetBool(TransitionParameters.Walk.ToString(), false);
                        player.getNavMeshAgent.isStopped = true;
                    }
                    else if (oType.objectType == ObjectType.Ground)
                    {
                        VirtualInpuManager.getInstance.isMoving = true;
                    }
                }

                //if (null == obj)
                //{
                //    animator.SetBool(TransitionParameters.Walk.ToString(), false);
                //    player.getNavMeshAgent.isStopped = true;
                //}
                //else
                //{
                //    GameObjectType oType = obj.GetComponent<GameObjectType>();
                //    if (oType.objectType == ObjectType.Enemy && player.isAttacking && player.isRightMouseClick)
                //    {
                //        VirtualInpuManager.getInstance.isMoving = true;
                //    }
                //}
            }
        }
    }
}

