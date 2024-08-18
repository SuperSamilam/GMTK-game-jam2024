using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;

    private void FixedUpdate()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0;
        rb.drag = 100000;
    }
}
