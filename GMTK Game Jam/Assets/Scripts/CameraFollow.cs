using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Vector3 offset = new Vector3(0, 2, -10);
    [SerializeField] float smoothTime = 0.25f;
    Vector3 vel = Vector3.zero;

    [SerializeField] Transform target;

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetPos = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref vel, smoothTime);
    }


}
