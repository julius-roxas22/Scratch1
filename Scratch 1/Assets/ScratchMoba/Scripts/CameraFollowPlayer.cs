using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DumbAssStudio
{
    public class CameraFollowPlayer : MonoBehaviour
    {
        private PlayerController character;
        public Vector3 offset;
        public float t;
        public float distance;

        private void Awake()
        {
            character = GameObject.FindObjectOfType<PlayerController>();
        }

        private void LateUpdate()
        {
            Vector3 tempPositon = character.transform.position + offset;
            transform.position = Vector3.Slerp(transform.position, tempPositon, t * Time.smoothDeltaTime);
            transform.LookAt(character.transform);
        }
    }
}
