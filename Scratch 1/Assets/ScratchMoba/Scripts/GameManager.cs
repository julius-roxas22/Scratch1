using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DumbAssStudio
{

    public class GameManager : Singleton<GameManager>
    {
        public List<Obstacle> gameObjectObstacleList = new List<Obstacle>();
        public List<PlayerController> playerList = new List<PlayerController>();
        private void Awake()
        {
            obstacle();
            player();
        }

        private void player()
        {
            PlayerController[] players = FindObjectsOfType<PlayerController>();
            foreach (PlayerController p in players)
            {
                if (!playerList.Contains(p))
                {
                    playerList.Add(p);
                }
            }
        }

        private void obstacle()
        {
            Obstacle[] obstacles = GameObject.FindObjectsOfType<Obstacle>();

            foreach (Obstacle o in obstacles)
            {
                if (!gameObjectObstacleList.Contains(o))
                {
                    gameObjectObstacleList.Add(o);
                }
            }
        }

        public PlayerController getPlayableCharacter()
        {
            foreach (PlayerController p in playerList)
            {
                if (p.GetComponent<ManualInput>().enabled)
                {
                    return p;
                }
            }
            return null;
        }

    }
}