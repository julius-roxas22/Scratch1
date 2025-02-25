using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float smoothRotation;
    public Vector3 targetPosition;
    public float movementSpeed;
    public bool isMoving;
    public float distanceToStop;

    public Transform gunTip;
    public float bulletSpeed;

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButtonDown(1))
        {
            if (Physics.Raycast(ray, out hit))
            {
                targetPosition = hit.point;
                targetPosition.y = transform.position.y;
                isMoving = true;
            }
        }

        //if (Input.GetMouseButton(0))
        //{
        //    GameObject obj = Instantiate(Resources.Load("Bullet", typeof(GameObject))) as GameObject;
        //    obj.transform.position = gunTip.transform.position;
        //    Vector3 targetPos = obj.transform.position - targetPosition;
        //    Vector3.MoveTowards(obj.transform.position, targetPos, bulletSpeed * Time.deltaTime);
        //}

        if (isMoving)
        {
            Vector3 direction = targetPosition - transform.position;
            direction.y = 0;
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothRotation * Time.deltaTime);
            }

            float distance = (targetPosition - transform.position).sqrMagnitude;

            if (distance < distanceToStop)
            {
                isMoving = false;
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime);
    }
}
