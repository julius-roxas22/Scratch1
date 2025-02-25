using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DumbAssStudio
{

    [CreateAssetMenu(fileName = "New Death Type", menuName = "Create Ability/DumbAssStudio/DeathType")]
    public class DeathAnimationType : ScriptableObject
    {
        public List<eBodyParts> bodyParts = new List<eBodyParts>();
        public RuntimeAnimatorController deathController;
    }
}

