using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DumbAssStudio
{
    public class DeathManager : Singleton<DeathManager>
    {
        private List<RuntimeAnimatorController> candidates = new List<RuntimeAnimatorController>();
        private DeathData death;

        private void InstantiateDeath()
        {
            if (null == death)
            {
                GameObject o = GameObjectLoader.CreatePrefab(GameObjectLoaderType.Death);
                death = o.GetComponent<DeathData>();
            }
        }

        public RuntimeAnimatorController getDeathType(eBodyParts bodyParts)
        {
            candidates.Clear();
            InstantiateDeath();

            foreach (DeathAnimationType death in death.deathType)
            {
                foreach (eBodyParts part in death.bodyParts)
                {
                    if (part == bodyParts)
                    {
                        candidates.Add(death.deathController);
                    }
                }
            }
            return candidates[Random.Range(0, candidates.Count)];
        }
    }
}


