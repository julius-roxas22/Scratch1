using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DumbAssStudio
{
    public enum PoolObjectType
    {
        ATTACKINFO,
    }

    public class PoolObjectLoader : MonoBehaviour
    {
        public static PoolObject InstantiatePoolObject(PoolObjectType objType)
        {
            GameObject obj = null;

            switch (objType)
            {
                case PoolObjectType.ATTACKINFO:
                    {
                        obj = Instantiate(Resources.Load("AttackInfo", typeof(GameObject))) as GameObject;
                        break;
                    }
            }

            return obj.GetComponent<PoolObject>();
        }
    }
}