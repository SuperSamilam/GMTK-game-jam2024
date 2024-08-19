using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

//Should provbely pop the bubble if you are flying for to long

public class PlayerMovement : MonoBehaviour
{
    [Header("Speeds")]
    [SerializeField] float speed;
    [SerializeField] float airSpeed;
    [SerializeField] float floatSpeed;
    [SerializeField] float moveSpeed;

    [Header("Jumps")]
    [SerializeField] float jumpPower;
    [SerializeField] float bubbleJumpPower;

    float xInput;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Transform groundCheck;
    [SerializeField] Transform blockCheck;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask blockLayer;
    [SerializeField] Animator animator;
    public bool isFacingRight = true;
    public bool blowing = false;
    public bool allowedToShoot = true;

    public enum PlayerState { Walking, Idle, Jump};

    [SerializeField] AreaEffector2D[] fans;
    [SerializeField] LayerMask fanLayerBlowing;
    [SerializeField] LayerMask fanLayerNormal;

    public PlayerState state;
    void Update()
    {
        if (!blowing)
        {
            foreach (var f in fans)
            {
                f.colliderMask = fanLayerNormal;
            }
            allowedToShoot = true;

                xInput = Input.GetAxisRaw("Horizontal");
                moveSpeed = speed;
            
        
            //Jump
            if (Input.GetButtonDown("Jump") && IsGrounded())
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            }
            else if (!IsGrounded())
            {
                moveSpeed = airSpeed; 
            }
        }
        else
        {
            xInput = 0;
            if (Input.GetButtonDown("Jump") && IsGrounded())
            {
                rb.velocity = new Vector2(rb.velocity.x, bubbleJumpPower);
            }
            else if (!IsGrounded())
            {
                allowedToShoot = false;
                xInput = Input.GetAxisRaw("Horizontal");
                foreach (var f in fans)
                {
                    f.colliderMask = fanLayerBlowing;
                }
            }
        }

        HandleState();
    }

    private void FixedUpdate()
    {
        //Walk
        if (!blowing)
        {
            rb.gravityScale = 8;
            float horizontalMovement = xInput * moveSpeed * Time.deltaTime * 10f;
                rb.velocity = new Vector2(horizontalMovement, rb.velocity.y);
        }
        else
        {
            rb.gravityScale = 3;
            if (!IsGrounded())
            {
                float horizontalMovement = xInput * floatSpeed * Time.deltaTime * 10f;

                    rb.velocity = new Vector2(horizontalMovement, rb.velocity.y);
                

                if (rb.velocity.y < 0f)
                {
                    rb.velocity = new Vector2(rb.velocity.x, 0);
                }
            }
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }

        Flip();
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

    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
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



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Star")
        {
            //Win
        }
    }


    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;

    //    Vector2 center = blockCheck.position;

    //    Gizmos.matrix = Matrix4x4.TRS(center, Quaternion.Euler(0, 0, 0), Vector3.one);
    //    Gizmos.DrawWireCube(Vector3.zero, new Vector2(0.1f, 1.11f));
    //}

}
