using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

//Smooth Camera Follow
//No Ledge boosting

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float walkSpeed;
    [SerializeField] float sprintSpeed;
    float moveSpeed;

    [Header("Camera")]
    [SerializeField] Camera cam;
    [SerializeField] float camMoveThres;
    bool moveCamera;

    [Header("Keys")]
    [SerializeField] KeyCode jumpKey;
    [SerializeField] KeyCode walkKey;

    float horizontal;
    public float jumpingPower = 16f;
    bool isFacingRight = true;

    [SerializeField] Rigidbody2D rb;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;

    void Start()
    {
    }

    void Update()
    {
        HandleWalk();
        HandleJump();
        HandleCamera();

        Flip();
    }

    void HandleWalk()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        if (IsGrounded() && Input.GetKey(walkKey))
        {
            moveSpeed = walkSpeed;
        }
        else
        {
            moveSpeed = sprintSpeed;
        }
    }
    void HandleJump()
    {
        if (Input.GetKeyDown(jumpKey) && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }
    }
    void HandleCamera()
    {
        if (Vector2.Distance(cam.transform.position, this.transform.position) > camMoveThres)
        {
            moveCamera = true;
        }
        if (moveCamera)
        {
            Vector2 p = Vector2.Lerp(cam.transform.position, transform.position, moveSpeed*Time.deltaTime);
            Vector3 pos = new Vector3(p.x, p.y, cam.transform.position.z);
            cam.transform.position = pos;
        }
    }
    //Ground Check
    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    //Movement Updaters
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
    }



    //Direction
    void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f) 
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }
    public Vector2 GetLookDirection()
    {
       if (isFacingRight)
        {
            return new Vector2(1, 0);
        }
        else
        {
            return new Vector2(-1, 0);
        }
    }
}
