using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DumbAssStudio
{
    public class CameraManager : Singleton<CameraManager>
    {
        private Camera cam;
        private PlayerController playableCharacter;

        public Camera GetCamera
        {
            get
            {
                if (null == cam)
                {
                    cam = GameObject.FindAnyObjectByType<Camera>();
                }
                return cam;
            }
        }

        private void Awake()
        {
            foreach (PlayerController p in GameManager.getInstance.playerList)
            {
                if (p.gameObject.activeInHierarchy)
                {
                    playableCharacter = p;
                }
            }
        }

        private void Update()
        {
            //RaycastHit hit;

            //if (Physics.Raycast(GetCamera.transform.position, Vector3.forward - (GetCamera.transform.position - playableCharacter.transform.position), out hit))
            //{
            //    Obstacle obstacle = hit.collider.gameObject.GetComponent<Obstacle>();

            //    if (null != obstacle)
            //    {
            //        if (obstacle.name == "Obstacle")
            //        {
            //            obstacleDataHolder = obstacle;
            //            obstacle.intensity = 0.1f;
            //        }
            //        else
            //        {
            //            obstacle.intensity = 1f;
            //        }
            //    }
            //    else
            //    {
            //        obstacleDataHolder.intensity = 1f; // not working!
            //    }
            //}
            //else
            //{
            //    obstacleDataHolder.intensity = 1f;
            //}

            RaycastHit hit;

            foreach (Obstacle o in GameManager.getInstance.gameObjectObstacleList)
            {
                if (Physics.Raycast(GetCamera.transform.position, Vector3.forward - (GetCamera.transform.position - playableCharacter.transform.position), out hit))
                {
                    Obstacle obstacle = hit.collider.transform.root.GetComponent<Obstacle>();
                    if (null != obstacle)
                    {
                        obstacle.opacityIntensity = 0.2f;
                    }
                }
                o.opacityIntensity = 1f;
            }

            Debug.DrawRay(GetCamera.transform.position, Vector3.forward - (GetCamera.transform.position - playableCharacter.transform.position), Color.red);
        }
    }

}