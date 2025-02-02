using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DumbAssStudio
{

    public enum ObjectType
    {
        Ground,
        Enemy,
        Allies,
        Tree,
        Pole
    }

    public class GameObjectType : MonoBehaviour
    {
        public ObjectType objectType;
    }
}
