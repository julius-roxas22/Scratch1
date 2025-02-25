using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DumbAssStudio
{
    public class MouseController : MonoBehaviour
    {
        public GameObject onHoverGameobject;
        //public GameObject selectedCharacter;

        public List<GameObject> allSelectedCharacter = new List<GameObject>();

        private void Awake()
        {
            if (null == onHoverGameobject)
            {
                foreach (PlayerController playerController in GameManager.getInstance.playerList)
                {
                    if (!allSelectedCharacter.Contains(playerController.gameObject))
                    {
                        allSelectedCharacter.Add(playerController.gameObject);
                    }
                }
            }
        }

        void Update()
        {
            Ray ray = CameraManager.getInstance.GetCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                onHoverGameobject = hit.transform.gameObject;
            }

            if (Input.GetMouseButton(0))
            {
                if (null != onHoverGameobject)
                {
                    if (!allSelectedCharacter.Contains(onHoverGameobject))
                    {
                        allSelectedCharacter.Add(onHoverGameobject);
                    }
                }
            }

            GameObject hitObjPt = null;

            if (Input.GetMouseButton(1))
            {
                foreach (GameObject obj in allSelectedCharacter)
                {
                    PlayerController playerController = obj.GetComponent<PlayerController>();
                    if (null != playerController)
                    {
                        playerController.forwardLook = true;
                        playerController.getTargetHitPoint = new Vector3(hit.point.x, 0f, hit.point.z);
                        VirtualInpuManager.getInstance.isWalking = true;
                        playerController.interactionObjectChecker(hit.collider);
                    }
                }

                if (null == hitObjPt)
                {
                    hitObjPt = Instantiate(Resources.Load("HitPoint", typeof(GameObject))) as GameObject;
                    hitObjPt.transform.position = new Vector3(hit.point.x, 0f, hit.point.z);
                    Destroy(hitObjPt, 2f);
                }
            }

            foreach (GameObject o in allSelectedCharacter)
            {
                PlayerController controller = o.GetComponent<PlayerController>();
                if (null != controller)
                {
                    Debug.DrawRay(o.transform.position, controller.getNavAgent.destination, Color.black);
                }
            }
        }
    }
}

