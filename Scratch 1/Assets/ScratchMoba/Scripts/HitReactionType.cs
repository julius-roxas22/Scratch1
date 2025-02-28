using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DumbAssStudio
{
    public enum HitType
    {
        None,
        LeadJab,
        Kick,
        CrossPunch
    }

    [CreateAssetMenu(fileName = "New Hit Reaction Type", menuName = "Create Data/DumbAssStudio/HitReaction")]
    public class HitReactionType : ScriptableObject
    {
        public HitType hitType;
        public RuntimeAnimatorController hitController;
    }

}
