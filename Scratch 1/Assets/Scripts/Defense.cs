using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DumbAssStudio
{
    public class Defense : MonoBehaviour
    {
        public float attackRange;
        public int maxHealth;
        public int currentHealth;

        private void Awake()
        {
            currentHealth = maxHealth;
        }
    }
}