using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DumbAssStudio
{
    public class DamageDetector : MonoBehaviour
    {
        public void takeDamage(int damage, PlayerController playerController)
        {
            Defense defense = playerController.getDefense;
            defense.currentHealth -= damage;
            if (defense.currentHealth <= 0)
            {
                Debug.Log(playerController.name + "s died");
            }
        }
    }
}

