using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    float horizontal;
    public float speed = 8f;
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
        horizontal = Input.GetAxisRaw("Horizontal");
         
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        Flip();
    }

    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

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

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
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


    //Hoppa
    //Gå 
    //Springa

}
