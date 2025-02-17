using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DumbAssStudio
{
    public class PoolObject : MonoBehaviour
    {
        public PoolObjectType objType;

        public void turnOff()
        {
            PoolManager.getInstance.addPoolObject(this);
        }
    }
}