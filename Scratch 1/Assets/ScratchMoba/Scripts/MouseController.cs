using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DumbAssStudio
{
    public class MouseController : MonoBehaviour
    {
        public GameObject onHoverGameobject;
        public GameObject selectedMovableCharacter;

        //private PlayerController playerController;
        //private void Awake()
        //{
        //    foreach (PlayerController p in GameManager.getInstance.playerList)
        //    {
        //        if (p.name.Equals("Player"))
        //        {
        //            playerController = p;
        //        }
        //    }
        //}

        void Update()
        {
            Ray ray = CameraManager.getInstance.GetCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                onHoverGameobject = hit.transform.gameObject;
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (null != onHoverGameobject)
                {
                    selectedMovableCharacter = onHoverGameobject;
                }
            }

            GameObject hitObjPt = null;

            if (Input.GetMouseButtonDown(1))
            {
                PlayerController playerController = selectedMovableCharacter.GetComponent<PlayerController>();
                if (null == playerController)
                {
                    return;
                }

                if (hit.collider.gameObject == playerController.gameObject)
                {
                    return;
                }

                playerController.forwardLook = true;
                playerController.getTargetHitPoint = new Vector3(hit.point.x, 0f, hit.point.z);
                VirtualInpuManager.getInstance.isWalking = true;
                playerController.interactionObjectChecker(hit.collider);

                if (null == hitObjPt)
                {
                    hitObjPt = GameObjectLoader.CreatePrefab(GameObjectLoaderType.HitPoint);
                    hitObjPt.transform.position = new Vector3(hit.point.x, 0f, hit.point.z);
                    Destroy(hitObjPt, 2f);
                }
            }
        }
    }
}

