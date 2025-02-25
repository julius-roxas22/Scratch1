using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DumbAssStudio
{
    public class NPC_Move : MonoBehaviour
    {
        private PlayerController playerController;
        private GameObjectType objType;

        private void Awake()
        {
            playerController = GetComponentInParent<PlayerController>();
        }

        private void Update()
        {
            foreach (PlayerController p in GameManager.getInstance.playerList)
            {
                if (p != playerController)
                {
                    objType = p.GetComponent<GameObjectType>();
                }
            }

            if (null != objType)
            {
                if (objType.objectType == ObjectType.Allies)
                {
                    playerController.forwardLook = true;
                    playerController.getTargetHitPoint = objType.gameObject.transform.position;
                    playerController.isWalking = true;
                }
            }
        }
    }
}

