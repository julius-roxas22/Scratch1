using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DumbAssStudio
{
    public class CustomInputSystem : MonoBehaviour
    {
        void Update()
        {
            VirtualInpuManager.getInstance.OnRightMouseButtonDown = Input.GetMouseButtonDown(1) ? true : false;
            VirtualInpuManager.getInstance.isStopMoving = Input.GetKey(KeyCode.S) ? true : false;
        }
    }
}