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
        public List<ObjectType> avoidObjectList = new List<ObjectType>();
        public List<GameObject> objHitPoints = new List<GameObject>();

        private NavMeshAgent agent;

        public bool isWalking;
        public bool isAttacking;
        public bool isStopMoving;
        public bool OnRightMouseButtonDown;

        public float stoppingDist;
        public float smoothTurningLookForward;

        private Vector3 targetHitPoint;
        private bool isTurningForwardLook;

        public Vector3 getTargetHitPoint
        {
            get
            {
                return targetHitPoint;
            }

            set
            {
                targetHitPoint = value;
            }
        }

        public NavMeshAgent getNavAgent
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
            Ray ray = CameraManager.getInstance.GetCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (OnRightMouseButtonDown)
            {
                if (Physics.Raycast(ray, out hit))
                {
                    isTurningForwardLook = true;

                    Vector3 hitP = hit.point;
                    hitP.y = 0;
                    getTargetHitPoint = hitP;

                    VirtualInpuManager.getInstance.isWalking = true;

                    GameObject objHitP = Instantiate(Resources.Load("HitPoint", typeof(GameObject))) as GameObject;
                    objHitP.transform.position = getTargetHitPoint;
                    Destroy(objHitP, 2f);
                }
            }

            float stoppingPointDist = (getTargetHitPoint - transform.position).sqrMagnitude;

            if (isTurningForwardLook)
            {
                Vector3 look = getTargetHitPoint - transform.position;
                look.y = 0f;

                if (look != Vector3.zero)
                {
                    Quaternion targetRotate = Quaternion.LookRotation(look);
                    Quaternion smoothRotate = Quaternion.Slerp(transform.rotation, targetRotate, smoothTurningLookForward * Time.deltaTime);
                    transform.rotation = smoothRotate;
                }
            }

            if (stoppingPointDist < stoppingDist)
            {
                VirtualInpuManager.getInstance.isWalking = false;
            }
        }

        public void moveTowardsTo(Vector3 destination)
        {
            getNavAgent.SetDestination(destination);
        }
    }
}
