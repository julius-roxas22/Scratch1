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
        private CharacterAttributes attributes;

        public List<ObjectType> gameObjectAvoidanceList = new List<ObjectType>();

        public GameObject interactionObject = null;
        public Vector3 offSetToAttack;
        public bool canlookRotate;

        public bool isWalking;
        public bool isAttacking;
        public bool isStopMoving;
        public bool rightMouseClick;

        //public Vector3 targetPosition;
        private Vector3 rayCastHitPoint;
        public bool isMoving;
        public float objectDistanceToStop;
        public float rotationSpeed;

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

            //playerInteractionObject();

            foreach (GameObject o in obj) // it simply destroy the hit point object
            {
                Destroy(o, 3f);
            }

        }

        private void mouseMovement()
        {
            Ray ray = CameraManager.getInstance.GetCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (rightMouseClick)
            {
                if (Physics.Raycast(ray, out hit))
                {
                    Vector3 targetPosition = hit.point;
                    targetPosition.y = transform.position.y;
                    setRayCastHitPoint(targetPosition);
                    VirtualInpuManager.getInstance.isMoving = true;
                }

                #region instantiate hit point object
                GameObject hitPoint = Instantiate(Resources.Load("HitPoint", typeof(GameObject))) as GameObject;

                hitPoint.transform.position = getRayCastHitPoint;

                obj.Add(hitPoint);
                #endregion
            }

            if (isMoving)
            {
                Vector3 dir = getRayCastHitPoint - transform.position;
                dir.y = 0;

                if (dir != Vector3.zero)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(dir);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                }

                float dist = (getRayCastHitPoint - transform.position).sqrMagnitude;

                if (dist < objectDistanceToStop)
                {
                    VirtualInpuManager.getInstance.isMoving = false;
                }
            }
        }

        public void playerMove(float movementSpeed)
        {
            transform.position = Vector3.MoveTowards(transform.position, getRayCastHitPoint, movementSpeed * Time.deltaTime);
        }

        //private void playerInteractionObject()
        //{
        //    PlayerController playerController = null;
        //    if (enabled)
        //    {
        //        playerController = this;
        //    }

        //    GameObject enemy = null;

        //    if (null != interactionObject)
        //    {
        //        enemy = interactionObject;
        //    }

        //    if (null == interactionObject)
        //    {
        //        return;
        //    }

        //    float dist = (enemy.transform.position - playerController.transform.position).sqrMagnitude;

        //    if (dist < getAttributes.attackRange)
        //    {
        //        getNavMeshAgent.isStopped = true;
        //        VirtualInpuManager.getInstance.isAttacking = true;
        //        getPlayerAnimatorProgress.isWalking = false;
        //        lookRotation(enemy, Vector3.up);
        //    }
        //    else
        //    {
        //        getNavMeshAgent.isStopped = false;
        //        VirtualInpuManager.getInstance.isAttacking = false;
        //        getPlayerAnimatorProgress.isWalking = true;
        //        lookRotation(enemy, Vector3.up);
        //    }
        //}

        private void checkValidToMove(Collider col)
        {
            if (gameObject == col.gameObject)
            {
                VirtualInpuManager.getInstance.isMoving = false;
            }

            //float dist = (col.gameObject.transform.position - transform.position).sqrMagnitude;

            //if (dist < objectDistanceToStop)
            //{
            //    VirtualInpuManager.getInstance.isMoving = false;
            //}

            //if (hit.collider.gameObject == this.gameObject)
            //{
            //    return;
            //}

            //GameObjectType gameObjectType = hit.collider.transform.root.GetComponent<GameObjectType>();

            //if (null == gameObjectType)
            //{
            //    return;
            //}

            //foreach (ObjectType objectType in gameObjectAvoidanceList)
            //{
            //    if (objectType.Equals(gameObjectType.objectType))
            //    {
            //        return;
            //    }
            //}

            //switch (gameObjectType.objectType)
            //{
            //    case ObjectType.Ground:
            //        {
            //            if (canlookRotate)
            //            {
            //                lookRotation(hit);
            //            }
            //            getPlayerAnimatorProgress.isWalking = true;
            //            interactionObject = null;
            //            VirtualInpuManager.getInstance.isAttacking = false;
            //            break;
            //        }
            //    case ObjectType.Enemy:
            //        {
            //            lookRotation(hit);
            //            interactionObject = gameObjectType.gameObject;
            //            break;
            //        }
            //}

            //setRayCastHitPoint(hit.point);
        }

    }
}
