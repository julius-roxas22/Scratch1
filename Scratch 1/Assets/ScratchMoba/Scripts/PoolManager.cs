using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DumbAssStudio
{
    public class PoolManager : Singleton<PoolManager>
    {
        public Dictionary<PoolObjectType, List<GameObject>> poolDictionary = new Dictionary<PoolObjectType, List<GameObject>>();

        private void setUpDictionary()
        {
            PoolObjectType[] arr = System.Enum.GetValues(typeof(PoolObjectType)) as PoolObjectType[];
            if (poolDictionary.Count == 0)
            {
                foreach (PoolObjectType p in arr)
                {
                    if (!poolDictionary.ContainsKey(p))
                    {
                        poolDictionary.Add(p, new List<GameObject>());
                    }
                }
            }
        }

        public GameObject InstantiatePoolObjectPrefab(PoolObjectType objType)
        {
            setUpDictionary();

            GameObject obj = null;

            List<GameObject> list = poolDictionary[objType];

            if (list.Count > 0)
            {
                obj = list[0];
                list.RemoveAt(0);
            }
            else
            {
                obj = PoolObjectLoader.InstantiatePoolObject(objType).gameObject;
            }

            return obj;
        }

        public void addPoolObject(PoolObject poolObject)
        {
            List<GameObject> list = poolDictionary[poolObject.objType];
            list.Add(poolObject.gameObject);
            poolObject.gameObject.SetActive(false);
        }
    }
}