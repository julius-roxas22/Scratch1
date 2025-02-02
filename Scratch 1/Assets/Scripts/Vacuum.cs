using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace DumbAssStudio
{
    public class Vaccum : MonoBehaviour
    {
        PlayerController character = null;
        public float sphereRadius;
        public float deltaSpeed;
        public float PullForce;

        private void Awake()
        {
            character = GameObject.FindObjectOfType<PlayerController>();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, sphereRadius);
        }

        private void FixedUpdate()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, sphereRadius);

            foreach (Collider col in colliders)
            {
                character = col.transform.root.GetComponent<PlayerController>();
                if (null != character)
                {
                    Rigidbody rb = character.GetComponent<Rigidbody>();
                    NavMeshAgent agent = character.GetComponent<NavMeshAgent>();

                    Vector3 distance = transform.position - character.transform.position;
                    agent.speed = 2f;
                    if (distance.sqrMagnitude > 2f)
                    {
                        rb.AddForce((transform.position - character.transform.position) * PullForce * Time.fixedDeltaTime);
                    }
                    else
                    {
                        character.transform.position = distance + transform.position;
                    }
                }
            }
        }
    }
}