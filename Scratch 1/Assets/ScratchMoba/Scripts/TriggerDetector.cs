using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DumbAssStudio
{
    public enum eBodyParts
    {
        Upper,
        Lower,
        Leg,
        Arm
    }
    public class TriggerDetector : MonoBehaviour
    {
        public List<Collider> collidingParts = new List<Collider>();
        public eBodyParts bodyParts;

        private PlayerController playerController;

        public PlayerController GetPlayerController
        {
            get
            {
                if (null == playerController)
                {
                    playerController = GetComponentInParent<PlayerController>();
                }
                return playerController;
            }
        }

        private void OnTriggerEnter(Collider col)
        {
            if (GetPlayerController.ragdollParts.Contains(col))
            {
                return;
            }

            PlayerController attacker = col.transform.root.GetComponent<PlayerController>();

            if (null == attacker)
            {
                return;
            }

            if (col.gameObject == attacker.gameObject)
            {
                return;
            }

            if (!collidingParts.Contains(col))
            {
                collidingParts.Add(col);
            }
        }

        private void OnTriggerExit(Collider col)
        {
            if (collidingParts.Contains(col))
            {
                collidingParts.Remove(col);
            }
        }
    }
}


