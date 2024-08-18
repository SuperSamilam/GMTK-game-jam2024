using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //Vector3 offset = new Vector3(0, 2, -10);
    //[SerializeField] float smoothTime = 0.5f;
    //Vector3 vel = Vector3.zero;

    //[SerializeField] Transform target;

    //// Update is called once per frame
    //void FixedUpdate()
    //{
    //    Vector3 targetPos = target.position + offset;
    //    transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref vel, smoothTime);
    //}

    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
