using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DumbAssStudio
{
    public enum GameObjectLoaderType
    {
        HitPoint,
        TrackingObject,
        Death,
        HitReaction
    }

    public class GameObjectLoader : MonoBehaviour
    {
        public static GameObject CreatePrefab(GameObjectLoaderType objType)
        {
            GameObject obj = null;

            switch (objType)
            {
                case GameObjectLoaderType.HitPoint:
                    {
                        obj = Instantiate(Resources.Load(GameObjectLoaderType.HitPoint.ToString(), typeof(GameObject))) as GameObject;
                        break;
                    }
                case GameObjectLoaderType.TrackingObject:
                    {
                        obj = Instantiate(Resources.Load(GameObjectLoaderType.TrackingObject.ToString(), typeof(GameObject))) as GameObject;
                        break;
                    }
                case GameObjectLoaderType.Death:
                    {
                        obj = Instantiate(Resources.Load(GameObjectLoaderType.Death.ToString(), typeof(GameObject))) as GameObject;
                        break;
                    }
                case GameObjectLoaderType.HitReaction:
                    {
                        obj = Instantiate(Resources.Load(GameObjectLoaderType.HitReaction.ToString(), typeof(GameObject))) as GameObject;
                        break;
                    }
            }

            return obj;
        }
    }
}

