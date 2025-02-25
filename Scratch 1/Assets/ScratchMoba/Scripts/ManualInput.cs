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
            playerController.isWalking = VirtualInpuManager.getInstance.isWalking;
            playerController.isAttacking = VirtualInpuManager.getInstance.isAttacking;
            playerController.onPressStop = VirtualInpuManager.getInstance.onPressStop;
            playerController.onRightMouseButtonDown = VirtualInpuManager.getInstance.onRightMouseButtonDown;
            //playerController.onLeftMouseButtonDown = VirtualInpuManager.getInstance.onLeftMouseButtonDown;
        }
    }
}


