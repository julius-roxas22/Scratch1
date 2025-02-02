using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace DumbAssStudio
{
    public enum TransitionParameters
    {
        Walk,
        SpellAttack1
    }

    public class PlayerController : MonoBehaviour
    {
        private NavMeshAgent agent;
        private PlayerAnimatorProgress playerAnimatorProgress;
        private Vector3 rayCastHitPoint;
        private CharacterAttributes attributes;

        public List<ObjectType> gameObjectAvoidanceList = new List<ObjectType>();

        private void setRayCastHitPoint(Vector3 point)
        {
            rayCastHitPoint = point;
        }

        public Vector3 GetRayCastHitPoint
        {
            get
            {
                return rayCastHitPoint;
            }
        }

        public CharacterAttributes GetAttributes
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

        public PlayerAnimatorProgress GetPlayerAnimatorProgress
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

        public NavMeshAgent GetNavMeshAgent
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

            Ray ray = CameraManager.GetInstance.GetCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (VirtualInpuManager.GetInstance.MouseRightClick)
            {
                if (Physics.Raycast(ray, out hit))
                {
                    CheckValidToMove(hit);
                }
            }

            foreach (GameObject o in obj)
            {
                Destroy(o, 3f); //temporary instantiate some hit point gameobject
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

        private void CheckValidToMove(RaycastHit hit)
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
                        GameManager.GetInstance.PlayerInteractionObject = null;
                        break;
                    }
                case ObjectType.Enemy:
                    {

                        lookRotation(hit);

                        float dist = (gameObjectType.transform.position - transform.position).sqrMagnitude;

                        GameManager.GetInstance.PlayerInteractionObject = gameObjectType.gameObject;

                        //if (dist < GetAttributes.attackRange)
                        //{
                        //    Debug.Log("player will attack instantly");
                        //    playerAnimatorProgress.IsWalking = false;

                        //    if (!playerAnimatorProgress.IsWalking && dist > GetAttributes.attackRange)
                        //    {
                        //        Debug.Log("Player is chasing the enemy to attack");
                        //    }
                        //}
                        //else
                        //{
                        //    playerAnimatorProgress.IsWalking = true;

                        //    if (dist < GetAttributes.attackRange && playerAnimatorProgress.IsWalking)
                        //    {
                        //        playerAnimatorProgress.IsWalking = false;

                        //        Debug.Log("player will start attacking the enemy when its already close to him");
                        //    }

                        //    Debug.Log("Player is too far from enemy and start to close to attack the enemy");
                        //}

                        //Debug.Log("distance between " + gameObjectType.name + " and " + gameObject.name + "  is =" + dist);

                        break;
                    }
            }

            GetNavMeshAgent.stoppingDistance = 0;
            setRayCastHitPoint(hit.point);
            //GameManager.GetInstance.PlayerInteractionObject = null;
        }
    }
}
