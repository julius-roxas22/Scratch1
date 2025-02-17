using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DumbAssStudio
{
    public class ManualInput : MonoBehaviour
    {
        private PlayerController playerController;
        private void Awake()
        {
            playerController = GetComponent<PlayerController>();
        }

        private void Update()
        {
            playerController.isWalking = playerController.getPlayerAnimatorProgress.isWalking;
            playerController.isAttacking = VirtualInpuManager.getInstance.isAttacking;
            playerController.isStopMoving = VirtualInpuManager.getInstance.isStopMoving;
            playerController.isRightMouseClick = VirtualInpuManager.getInstance.rightMouseClick;
            playerController.isMoving = VirtualInpuManager.getInstance.isMoving;
        }
    }
}


