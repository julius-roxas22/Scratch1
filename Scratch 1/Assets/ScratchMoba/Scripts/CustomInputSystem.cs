using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DumbAssStudio
{
    public class CustomInputSystem : MonoBehaviour
    {
        void Update()
        {
            VirtualInpuManager.getInstance.onRightMouseButtonDown = Input.GetMouseButtonDown(1) ? true : false;
            VirtualInpuManager.getInstance.onLeftMouseButtonDown = Input.GetMouseButtonDown(0) ? true : false;
            VirtualInpuManager.getInstance.onPressStop = Input.GetKey(KeyCode.S) ? true : false;
        }
    }
}