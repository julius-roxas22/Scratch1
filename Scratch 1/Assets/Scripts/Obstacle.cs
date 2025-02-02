using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DumbAssStudio
{


    public class Obstacle : MonoBehaviour
    {
        [Range(0f, 1f)]
        public float opacityIntensity;
        public LayerMask layerMask;
        public float radiusCloseToPlayer;

        private Renderer myMaterial;

        void Awake()
        {
            myMaterial = GetComponent<Renderer>();
        }

        private void Update()
        {
            //if (Physics.CheckSphere(transform.position, radiusCloseToPlayer, layerMask))
            //{
            //    intensity = 0.2f;
            //}
            //else
            //{
            //    intensity = 1f;
            //}

            if (null != myMaterial)
            {
                myMaterial.material.color = new Color(myMaterial.material.color.r, myMaterial.material.color.g, myMaterial.material.color.b, opacityIntensity);
            }
        }
    }
}