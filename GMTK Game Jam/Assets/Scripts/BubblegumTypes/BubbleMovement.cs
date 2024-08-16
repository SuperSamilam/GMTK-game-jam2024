using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class BubbleMovement : MonoBehaviour
{
    Vector2 direction;
    [SerializeField] float speed;
    [SerializeField] float maxBounces = 3;

    Rigidbody2D rb;
    float bounces;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    public void SetupBubble(Vector2 dir)
    {
        direction = dir;
        //rb.velocity = new Vector2(direction.x * speed, direction.y * speed);
    }

    void Update()
    {
            rb.velocity = new Vector2(direction.x * speed, direction.y * speed);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Button")
        {
            collision.gameObject.GetComponent<ButtonController>().Press();
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.tag != "Player")
        {
            direction = Vector2.Reflect(direction, collision.contacts[0].normal);
            bounces++;
            if (bounces > maxBounces)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
