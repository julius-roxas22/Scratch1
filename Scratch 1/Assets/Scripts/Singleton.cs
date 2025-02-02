using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DumbAssStudio
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T Instance;

        public static T GetInstance
        {
            get
            {
                if (null == Instance)
                {
                    GameObject obj = new GameObject();
                    Instance = obj.AddComponent<T>();
                    obj.name = typeof(T).ToString();
                }
                return Instance;
            }
        }
    }
}