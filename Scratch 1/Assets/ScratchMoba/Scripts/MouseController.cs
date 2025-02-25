using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DumbAssStudio
{
    public class MouseController : MonoBehaviour
    {
        public GameObject onHoverGameobject;
        public GameObject selectedCharacter;

        private void Awake()
        {
            if (null == onHoverGameobject)
            {
                foreach (PlayerController p in GameManager.getInstance.playerList)
                {
                    if (p.name.Contains("Player"))
                    {
                        selectedCharacter = p.gameObject;
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
                    selectedCharacter = onHoverGameobject;
                }
            }

            if (Input.GetMouseButton(1))
            {
                PlayerController playerController = selectedCharacter.GetComponent<PlayerController>();
                if (null != playerController)
                {
                    playerController.forwardLook = true;
                    playerController.getTargetHitPoint = new Vector3(hit.point.x, 0f, hit.point.z);
                    VirtualInpuManager.getInstance.isWalking = true;
                    playerController.interactionObjectChecker(hit.collider);
                }
            }
        }
    }

}

