using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace DumbAssStudio
{
    public enum TransitionParameters
    {
        Walk,
        SpellAttack1,
        Normal_Attack,
        ForceTransition,
    }

    public class PlayerController : MonoBehaviour
    {
        private NavMeshAgent agent;
        private PlayerAnimatorProgress playerAnimatorProgress;
        private Vector3 rayCastHitPoint;
        private CharacterAttributes attributes;

        public List<ObjectType> gameObjectAvoidanceList = new List<ObjectType>();

        public GameObject interactionObject = null;
        public Vector3 offSetToAttack;

        public void setRayCastHitPoint(Vector3 point)
        {
            rayCastHitPoint = point;
        }

        public Vector3 getRayCastHitPoint
        {
            get
            {
                return rayCastHitPoint;
            }
        }

        public CharacterAttributes getAttributes
        {
            get
            {
                if (null == attributes)
                {
                    attributes = GetComponent<CharacterAttributes>();
                }
                return attributes;
            }
        }

        public PlayerAnimatorProgress getPlayerAnimatorProgress
        {
            get
            {
                if (null == playerAnimatorProgress)
                {
                    playerAnimatorProgress = GetComponent<PlayerAnimatorProgress>();
                }
                return playerAnimatorProgress;
            }
        }

        public NavMeshAgent getNavMeshAgent
        {
            get
            {
                if (null == agent)
                {
                    agent = GetComponent<NavMeshAgent>();
                }
                return agent;
            }
        }

        public List<GameObject> obj = new List<GameObject>();

        private void Update()
        {
            mouseMovement();

            playerInteractionObject();

            foreach (GameObject o in obj)
            {
                Destroy(o, 3f); //temporary instantiate some hit point gameobject
            }
        }

        private void mouseMovement()
        {
            Ray ray = CameraManager.getInstance.GetCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (VirtualInpuManager.getInstance.mouseRightClick)
            {
                if (Physics.Raycast(ray, out hit))
                {
                    checkValidToMove(hit);
                }
            }
        }

        private void playerInteractionObject()
        {
            PlayerController playerController = null;
            if (enabled)
            {
                playerController = this;
            }

            GameObject enemy = null;

            if (null != interactionObject)
            {
                enemy = interactionObject;
            }

            if (null == interactionObject)
            {
                return;
            }

            float dist = (enemy.transform.position - playerController.transform.position).sqrMagnitude;

            if (dist < getAttributes.attackRange)
            {
                getNavMeshAgent.isStopped = true;
                VirtualInpuManager.getInstance.isAttacking = true;
                getPlayerAnimatorProgress.IsWalking = false;
                lookRotation(enemy, Vector3.up);
            }
            else
            {
                getNavMeshAgent.isStopped = false;
                VirtualInpuManager.getInstance.isAttacking = false;
                getPlayerAnimatorProgress.IsWalking = true;
                lookRotation(enemy, Vector3.up);

                if (!VirtualInpuManager.getInstance.isAttacking) setRayCastHitPoint(enemy.transform.position);
            }
        }

        private void lookRotation(RaycastHit hit)
        {
            GameObject hitPoint = Instantiate(Resources.Load("HitPoint", typeof(GameObject))) as GameObject;

            hitPoint.transform.position = hit.point;

            obj.Add(hitPoint);

            Vector3 lookDir = hit.point - transform.position;
            lookDir.y = 0;

            Quaternion lookRotation = Quaternion.LookRotation(lookDir, Vector3.up);

            //Quaternion smoothRotate = Quaternion.Lerp(transform.rotation, lookRotation, smoothRotation);

            transform.rotation = /*Quaternion.LookRotation(lookDir, Vector3.up);*/ lookRotation;
        }

        public void lookRotation(GameObject objectPosition, Vector3 upwardSet)
        {
            Vector3 lookDir = objectPosition.transform.position - transform.position;
            lookDir.y = 0;

            Quaternion lookRotate = Quaternion.LookRotation(lookDir, upwardSet);
            transform.rotation = lookRotate;
        }

        private void checkValidToMove(RaycastHit hit)
        {
            if (hit.collider.gameObject == this.gameObject)
            {
                return;
            }

            GameObjectType gameObjectType = hit.collider.transform.root.GetComponent<GameObjectType>();

            if (null == gameObjectType)
            {
                return;
            }

            foreach (ObjectType objectType in gameObjectAvoidanceList)
            {
                if (objectType.Equals(gameObjectType.objectType))
                {
                    return;
                }
            }

            switch (gameObjectType.objectType)
            {
                case ObjectType.Ground:
                    {
                        lookRotation(hit);
                        playerAnimatorProgress.IsWalking = true;
                        interactionObject = null;
                        VirtualInpuManager.getInstance.isAttacking = false;
                        break;
                    }
                case ObjectType.Enemy:
                    {
                        lookRotation(hit);
                        interactionObject = gameObjectType.gameObject;
                        break;
                    }
            }

            setRayCastHitPoint(hit.point);
        }

    }
}
