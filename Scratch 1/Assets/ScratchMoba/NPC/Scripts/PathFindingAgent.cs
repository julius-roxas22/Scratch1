using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DumbAssStudio
{
    public class PathFindingAgent : StateMachineBehaviour
    {
        private GameObject startPosition;
        private GameObject endPosition;
        private PlayerController playerController;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            foreach (PlayerController p in GameManager.getInstance.playerList)
            {
                if (p.name.Equals("Player"))
                {
                    playerController = p;
                }
            }

            if (null == endPosition)
            {
                endPosition = GameObjectLoader.CreatePrefab(GameObjectLoaderType.TrackingObject);
                endPosition.transform.position = playerController.transform.position;
                endPosition.name = "Target Position";
            }

            if (null == startPosition)
            {
                startPosition = GameObjectLoader.CreatePrefab(GameObjectLoaderType.TrackingObject);
                startPosition.transform.position = animator.gameObject.transform.position;
                startPosition.name = "Start Position";
            }
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

        }
    }
}


