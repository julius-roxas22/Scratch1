using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DumbAssStudio
{
    public class CustomInputSystem : MonoBehaviour
    {
        void Update()
        {
            VirtualInpuManager.GetInstance.MouseRightClick = Input.GetMouseButtonDown(1) ? true : false;
            VirtualInpuManager.GetInstance.IsStopMoving = Input.GetKey(KeyCode.S) ? true : false;
        }
    }
}