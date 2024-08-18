using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public float speed = 6f;
    public float size;
    public Vector2 dir;
    public Rigidbody2D rb;
    public AnimationCurve massCurve;
    public AnimationCurve speedCurve;

    public void Setup(float rsize, Vector2 rdir)
    {
        size = rsize;
        dir = rdir;

        transform.localScale = Vector3.one * size;
        rb.mass = massCurve.Evaluate(size);
        speed = speedCurve.Evaluate(size);

        rb.velocity = dir * speed;
    }

    void Pop()
    {
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Block")
        {
            Rigidbody2D crb = collision.gameObject.GetComponent<Rigidbody2D>();
            crb.AddForce(new Vector2(rb.mass * Mathf.Sign(dir.x), 0), ForceMode2D.Impulse);
            Pop();
        }
        if (collision.gameObject.tag == "Spike")
        {
            Pop();
        }
        else//Just bounce
        {
            Debug.Log(collision.gameObject.name);
            dir = Vector2.Reflect(dir, collision.contacts[0].normal);
        }
    }
}
