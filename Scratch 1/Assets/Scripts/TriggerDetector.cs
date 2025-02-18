using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DumbAssStudio
{
    public class TriggerDetector : MonoBehaviour
    {
        public List<Collider> collidingParts = new List<Collider>();

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

        private void OnCollisionEnter(Collision col)
        {

        }

        private void OnCollisionExit(Collision col)
        {

        }
    }
}


