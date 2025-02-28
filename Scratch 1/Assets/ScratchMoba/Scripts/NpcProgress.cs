using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DumbAssStudio
{
    public enum NpcTransitionType
    {
        None,
        Walk,
        Attack
    }

    public class NpcProgress : MonoBehaviour
    {
        [SerializeField] private NpcSubset[] getAllSubset;
        public GameObject attacker;

        private PlayerController playerController;

        private void Awake()
        {
            getAllSubset = GetComponentsInChildren<NpcSubset>();

            foreach (NpcSubset s in getAllSubset)
            {
                s.gameObject.SetActive(false);
            }
        }

        public PlayerController getPlayableController()
        {
            if (null == playerController)
            {
                foreach (PlayerController p in GameManager.getInstance.playerList)
                {
                    if (p.GetComponent<ManualInput>().enabled)
                    {
                        playerController = p;
                    }
                }
            }
            return playerController;
        }

        public NpcSubset getNpcTransitionSubset(NpcTransitionType type)
        {
            NpcSubset subset = null;

            foreach (NpcSubset s in getAllSubset)
            {
                if (s.npcTransitionType == type)
                {
                    subset = s;
                }
            }

            return subset;
        }
    }
}