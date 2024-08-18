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
    public AnimationCurve sizeCurve;
    private void Start()
    {

    }

    public void Setup(float rsize, Vector2 rdir)
    {
        size = rsize;
        dir = rdir;
        transform.localScale = Vector3.one * size;
        rb.mass = sizeCurve.Evaluate(size);
        rb.velocity = dir * speed;
    }

    // Update is called once per frame
    void Update()
    {


    }

    void Pop()
    {
        Destroy(this.gameObject);
    }

    private void FixedUpdate()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MoveBlock")
        {
            Pop();
            Debug.Log("coll");
        }
        else//Just bounce
        {
            Debug.Log(collision.gameObject.name);
            dir = Vector2.Reflect(dir, collision.contacts[0].normal);
        }
    }






}
