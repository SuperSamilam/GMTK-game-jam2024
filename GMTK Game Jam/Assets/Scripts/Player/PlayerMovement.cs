using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

//Should provbely pop the bubble if you are flying for to long

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 5;
    [SerializeField] float airSpeed = 8;
    [SerializeField] float floatSpeed = 3;
    float moveSpeed;

    [SerializeField] float jumpPower = 16f;
    public bool isFacingRight = true;
    public bool alowedToMove = true;
    public float bubbleRadius = 0;

    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator animator;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;

    float xInput;

    PlayerState state;
    enum PlayerState { Walking, Idle, Jump}

    void Update()
    {
        if (alowedToMove)
        {
            rb.gravityScale = 8;
            xInput = Input.GetAxis("Horizontal");
            moveSpeed = speed;
            if (Input.GetButtonDown("Jump") && IsGrounded())
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            }
            else if (!IsGrounded()) 
            {
                moveSpeed = airSpeed;
            }

            Flip();
        }
        else
        {
            if (IsGrounded())
            {
                xInput = 0;
                if (Input.GetButtonDown("Jump") && IsGrounded())
                {
                    rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(jumpPower * bubbleRadius, jumpPower
                        ));
                }
            }
            else//Not grounded
            {
                xInput = Input.GetAxis("Horizontal");
                rb.gravityScale = 3;
            }
        }

        HandleState();
    }

    void HandleState()
    {
        state = PlayerState.Idle;
        if (xInput != 0)
        {
            state = PlayerState.Walking;
        }
        if (!IsGrounded())
        {
            state = PlayerState.Jump;
        }

        if (state == PlayerState.Walking)
        {
            animator.SetBool("Walking", true);
        }
        else
        {
            animator.SetBool("Walking", false);
        }
    }

    private void FixedUpdate()
    {
        if (alowedToMove)
        {
            Vector2 direction = new Vector2(xInput, 0);
            rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);
        }
        else
        {
            if (IsGrounded())
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(xInput * floatSpeed, rb.velocity.y);
                if (rb.velocity.y < 0)
                {
                    rb.velocity = new Vector2(rb.velocity.x, 0);
                }
            }

        }
    }



    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
    }
    void Flip()
    {
        if (isFacingRight && xInput < 0f || !isFacingRight && xInput > 0)
        {
            isFacingRight = !isFacingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
        }
    }

}
