using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DumbAssStudio
{
    public enum ObjectType
    {
        None,
        Enemy,
        Allies,
        Pole,
        Tree,
        Ground
    }

    public class GameObjectType : MonoBehaviour
    {
        public ObjectType objectType;
    }
}
