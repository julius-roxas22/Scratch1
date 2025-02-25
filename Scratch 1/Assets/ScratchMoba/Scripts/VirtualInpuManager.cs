using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DumbAssStudio
{
    public class VirtualInpuManager : Singleton<VirtualInpuManager>
    {
        public bool onLeftMouseButtonDown;
        public bool onRightMouseButtonDown;
        public bool onPressStop;
        public bool isAttacking;
        public bool isWalking;
    }
}
