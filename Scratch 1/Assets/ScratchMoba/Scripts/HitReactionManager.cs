using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DumbAssStudio
{
    public class HitReactionManager : Singleton<HitReactionManager>
    {
        private List<RuntimeAnimatorController> candidates = new List<RuntimeAnimatorController>();
        private HitReactionData hitReactionData;

        private void setUpHitReactionData()
        {
            if (null == hitReactionData)
            {
                GameObject obj = GameObjectLoader.CreatePrefab(GameObjectLoaderType.HitReaction);
                hitReactionData = obj.GetComponent<HitReactionData>();
            }
        }

        public RuntimeAnimatorController getHitAnimatorController(HitType hitType)
        {
            candidates.Clear();

            setUpHitReactionData();

            foreach (HitReactionType hit in hitReactionData.hitReactionTypes)
            {
                if (hit.hitType == hitType)
                {
                    return hit.hitController;
                }
            }

            return null;
        }
    }
}

