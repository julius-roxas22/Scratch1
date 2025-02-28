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
            playerController.isWalking = VirtualInpuManager.getInstance.isWalking ? true : false;
            playerController.isAttacking = VirtualInpuManager.getInstance.isAttacking ? true : false;
            playerController.onPressStop = VirtualInpuManager.getInstance.onPressStop ? true : false;
            playerController.onRightMouseButtonDown = VirtualInpuManager.getInstance.onRightMouseButtonDown ? true : false;
            //playerController.onLeftMouseButtonDown = VirtualInpuManager.getInstance.onLeftMouseButtonDown;
        }
    }
}


