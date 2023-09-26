using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;
    [SerializeField] private float maxDeathZone = 1f;
    [SerializeField] private float speed = 1;

    private Vector3 ofssetPosition;
    private bool isMove = false;
    private Vector3 targetPosition;

    private void Start()
    {
        ofssetPosition = transform.position - targetTransform.position;
    }

    private void FixedUpdate()
    {
        targetPosition = targetTransform.position + ofssetPosition;

        if (isMove)
        {
            Move();
        }
        else
        {
            if (Vector3.Distance(transform.position, targetPosition) > maxDeathZone)
            {
                isMove = true;
            }
        }
    }


    private void Move()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.fixedDeltaTime);
        if (Vector3.Distance(transform.position,targetPosition) < 0.1f)
        {
            isMove = false;
        }
    }
}
