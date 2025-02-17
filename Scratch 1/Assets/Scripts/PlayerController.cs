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
        private Defense attributes;
        private ManualInput manualInput;

        public List<GameObject> obj = new List<GameObject>();
        public List<ObjectType> gameObjectAvoidanceList = new List<ObjectType>();
        public GameObject interactionObject;

        public bool isWalking;
        public bool isAttacking;
        public bool isStopMoving;
        public bool isMoving;
        public bool isRightMouseClick;

        private RaycastHit targetHit;
        public float notWalkablePathDistance;

        private Vector3 rayCastHitPoint;
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

        private ManualInput getManualInput
        {
            get
            {
                if (null == manualInput)
                {
                    manualInput = GetComponent<ManualInput>();
                }
                return manualInput;
            }
        }

        public Defense getAttributes
        {
            get
            {
                if (null == attributes)
                {
                    attributes = GetComponent<Defense>();
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

        private void Update()
        {
            mouseMovement();

            playerInteractionObject();

            foreach (GameObject o in obj) // it simply destroy the hit point object
            {
                Destroy(o, 3f);
            }
        }

        public float tempStopDist;

        private void mouseMovement()
        {
            Ray ray = CameraManager.getInstance.GetCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (isRightMouseClick)
            {
                if (Physics.Raycast(ray, out hit))
                {
                    Vector3 targetPosition = hit.point;
                    targetPosition.y = transform.position.y;
                    setRayCastHitPoint(targetPosition);
                    VirtualInpuManager.getInstance.isMoving = true;
                    targetHit = hit;
                    OnRightMousePress(targetHit.collider.gameObject);
                }

                #region instantiate hit point object
                GameObject hitPoint = Instantiate(Resources.Load("HitPoint", typeof(GameObject))) as GameObject;

                hitPoint.name = gameObject.name + " hit point move path";
                hitPoint.transform.position = getRayCastHitPoint;

                obj.Add(hitPoint);
                #endregion
            }
            float dist = (getRayCastHitPoint - transform.position).sqrMagnitude;
            if (dist < tempStopDist)
            {
                VirtualInpuManager.getInstance.isMoving = false;
            }

            if (isMoving)
            {
                lookRotation();
            }
        }

        public void lookRotation()
        {
            Vector3 lookDir = getRayCastHitPoint - transform.position;
            lookDir.y = 0;

            if (lookDir != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(lookDir);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }

        private void OnRightMousePress(GameObject obj)
        {
            if (null == obj)
            {
                return;
            }

            GameObjectType type = obj.GetComponent<GameObjectType>();

            if (null == type)
            {
                return;
            }

            switch (type.objectType)
            {
                case ObjectType.Enemy:
                    {
                        interactionObject = obj;
                        break;
                    }
                case ObjectType.Ground:
                    {
                        interactionObject = null;
                        VirtualInpuManager.getInstance.isAttacking = false;
                        return;
                    }
            }

            foreach (ObjectType oType in gameObjectAvoidanceList)
            {
                if (type.objectType == oType)
                {
                    float notWalkablePath = (obj.transform.position - transform.position).sqrMagnitude;

                    if (notWalkablePath < notWalkablePathDistance)
                    {
                        VirtualInpuManager.getInstance.isMoving = false;
                    }
                }
            }
        }

        public void playerMove(float movementSpeed, Vector3 target)
        {
            getNavMeshAgent.speed = movementSpeed;
            getNavMeshAgent.SetDestination(target);
        }

        private void playerInteractionObject()
        {
            ManualInput activeManualInput = null;
            if (getManualInput.enabled)
            {
                activeManualInput = getManualInput;
            }

            PlayerController playerController = null;

            if (null != activeManualInput)
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

            VirtualInpuManager.getInstance.isAttacking = dist < getAttributes.attackRange ? true : false;

            if (isAttacking)
            {
                VirtualInpuManager.getInstance.isMoving = false;
            }
            else
            {
                VirtualInpuManager.getInstance.isMoving = true;
            }
        }
    }
}
