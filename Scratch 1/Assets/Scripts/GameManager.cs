using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.AI;

namespace DumbAssStudio
{

    public class GameManager : Singleton<GameManager>
    {
        public List<Obstacle> gameObjectObstacleList = new List<Obstacle>();
        public List<PlayerController> playerList = new List<PlayerController>();
        public GameObject PlayerInteractionObject = null;

        private void Awake()
        {
            Obstacle();
            Player();

            //foreach (Player p in playerList)
            //{
            //    p.GetNavMeshAgent.angularSpeed = 0f;
            //    p.GetNavMeshAgent.acceleration = 0f;
            //}
        }

        private void Update()
        {
            PlayerController player = null;

            foreach (PlayerController p in playerList)
            {
                if (p.enabled)
                {
                    player = p;
                }
            }

            GameObject enemy = null;

            if (null != PlayerInteractionObject)
            {
                enemy = PlayerInteractionObject;
            }

            float dist = (enemy.transform.position - player.transform.position).sqrMagnitude;

            Debug.Log("distance between " + enemy.name + " and " + player.name + "  is =" + dist);
        }

        private void Player()
        {
            PlayerController[] players = GameManager.FindObjectsOfType<PlayerController>();
            foreach (PlayerController p in players)
            {
                playerList.Add(p);
            }
        }

        private void Obstacle()
        {
            Obstacle[] obstacles = GameObject.FindObjectsOfType<Obstacle>();

            foreach (Obstacle o in obstacles)
            {
                gameObjectObstacleList.Add(o);
            }
        }
    }
}