using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DumbAssStudio
{
    public class AttackManager : Singleton<AttackManager>
    {
        public List<AttackInfo> currentAttacks = new List<AttackInfo>();
    }
}