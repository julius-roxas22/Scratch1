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

    public enum AttackType
    {
        
    }

    public class PlayerController : MonoBehaviour
    {
        public List<ObjectType> avoidObjectList = new List<ObjectType>();
        public List<GameObject> objHitPoints = new List<GameObject>();
        public List<Collider> ragdollParts = new List<Collider>();
        public List<GameObject> objectCollidingParts = new List<GameObject>();
        public GameObject interactionObject;

        private List<TriggerDetector> triggerDetectors = new List<TriggerDetector>();

        public bool isWalking;
        public bool isAttacking;
        public bool onPressStop;
        public bool OnRightMouseButtonDown;
        public float stoppingDist;
        public float smoothTurningLookForward;
        public bool forwardLook;

        private BoxCollider boxCollider;
        private Animator skinnedMesh;
        private NavMeshAgent agent;
        private ManualInput manualInput;
        private DamageDetector damageDetector;
        private Defense defense;
        private Vector3 targetHitPoint;

        public ManualInput getManualInput
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

        public BoxCollider getBoxCollider
        {
            get
            {
                if (null == boxCollider)
                {
                    boxCollider = GetComponent<BoxCollider>();
                }
                return boxCollider;
            }
        }

        public Animator getSkinnedMesh
        {
            get
            {
                if (null == skinnedMesh)
                {
                    skinnedMesh = GetComponentInChildren<Animator>();
                }
                return skinnedMesh;
            }
        }

        public Defense getDefense
        {
            get
            {
                if (null == defense)
                {
                    defense = GetComponent<Defense>();
                }
                return defense;
            }
        }

        public DamageDetector getDamageDetector
        {
            get
            {
                if (null == damageDetector)
                {
                    damageDetector = GetComponent<DamageDetector>();
                }
                return damageDetector;
            }
        }

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

        public List<TriggerDetector> getAllTriggers()
        {
            TriggerDetector[] triggers = GetComponentsInChildren<TriggerDetector>();

            foreach (TriggerDetector t in triggers)
            {
                if (!triggerDetectors.Contains(t))
                {
                    triggerDetectors.Add(t);
                }
            }

            return triggerDetectors;
        }

        private void Update()
        {
            Ray ray = CameraManager.getInstance.GetCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (OnRightMouseButtonDown)
            {
                if (Physics.Raycast(ray, out hit))
                {
                    forwardLook = true;

                    Vector3 hitP = hit.point;
                    hitP.y = 0;
                    getTargetHitPoint = hitP;

                    VirtualInpuManager.getInstance.isWalking = true;

                    GameObject objHitP = Instantiate(Resources.Load("HitPoint", typeof(GameObject))) as GameObject;
                    objHitP.transform.position = getTargetHitPoint;
                    Destroy(objHitP, 2f);

                    interactionObjectChecker(hit.collider);
                }
            }

            if (forwardLook)
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

            float stoppingPointDist = (getTargetHitPoint - transform.position).sqrMagnitude;

            if (stoppingPointDist < stoppingDist)
            {
                VirtualInpuManager.getInstance.isWalking = false;
            }

            onEnemyHit();
        }

        public void setUpRagdoll()
        {
            ragdollParts.Clear();
            Collider[] col = GetComponentsInChildren<Collider>();

            foreach (Collider c in col)
            {
                if (c.gameObject != gameObject)
                {
                    c.isTrigger = true;
                    ragdollParts.Add(c);
                    if (null == c.GetComponent<TriggerDetector>())
                    {
                        c.gameObject.AddComponent<TriggerDetector>();
                    }
                }
            }
        }

        //private IEnumerator Start()
        //{
        //    yield return new WaitForSeconds(3f);
        //    turnOnRagdoll();
        //    GetComponent<BoxCollider>().enabled = false;
        //    getNavAgent.enabled = false;
        //    GetComponentInChildren<Animator>().enabled = false;
        //    GetComponentInChildren<Animator>().avatar = null;
        //}

        public void turnOnRagdoll()
        {
            foreach (Collider c in ragdollParts)
            {
                c.isTrigger = false;
                c.attachedRigidbody.velocity = Vector3.zero;
            }
        }

        private void interactionObjectChecker(Collider col)
        {
            GameObjectType objType = col.transform.root.GetComponent<GameObjectType>();

            switch (objType.objectType)
            {
                case ObjectType.Enemy:
                    {
                        interactionObject = objType.gameObject;
                        break;
                    }
                default:
                    {
                        interactionObject = null;
                        VirtualInpuManager.getInstance.isAttacking = false;
                        break;
                    }
            }
        }

        private void onEnemyHit()
        {
            PlayerController playerActive = null;

            if (getManualInput.enabled)
            {
                playerActive = this;
            }

            GameObject enemy = null;

            if (null != interactionObject)
            {
                enemy = interactionObject;
                instantLookAround();
            }

            if (null == enemy)
            {
                return;
            }

            float dist = (enemy.transform.position - playerActive.transform.position).sqrMagnitude;

            if (dist < getDefense.attackRange)
            {
                VirtualInpuManager.getInstance.isAttacking = true;
            }
            else
            {
                VirtualInpuManager.getInstance.isAttacking = false;
                VirtualInpuManager.getInstance.isWalking = true;
                getTargetHitPoint = enemy.transform.position;
            }
        }

        private void instantLookAround()
        {
            Vector3 lookAt = interactionObject.transform.position - transform.position;
            lookAt.y = 0f;
            transform.rotation = Quaternion.LookRotation(lookAt);
        }

        public void moveTowardsTo(Vector3 destination)
        {
            getNavAgent.SetDestination(destination);
        }
    }
}
