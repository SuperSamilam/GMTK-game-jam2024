using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBubbleGum : MonoBehaviour
{
    [SerializeField] BubbleMovement bubbleMovement;
    [SerializeField] int maxBounces = 3;
    private void Start()
    {
        bubbleMovement.BounceEvent += HandleBounce;
        bubbleMovement.TriggerEvent += HandleTrigger;
    }

    void HandleBounce(Collision2D col)
    {
        //Where does it collide
        //Player = pop, Wall = bounce if not to many bounces, Sticky Wall, Stick to it

        if (col.gameObject.tag == "Player")
        {
            bubbleMovement.popWithAnimation();
        }
        else if (col.gameObject.tag == "MoveBlock")
        {
            Rigidbody2D crb  = col.gameObject.GetComponent<Rigidbody2D>();
            crb.constraints = RigidbodyConstraints2D.None;
            crb.AddForce(Vector2.right * 100, ForceMode2D.Force);
            crb.constraints = RigidbodyConstraints2D.FreezeAll;


            bubbleMovement.popWithAnimation();
        }

        else
        {
            if (bubbleMovement.bounces >= maxBounces)
            {
                bubbleMovement.popWithAnimation();
            }
            else
            {
                bubbleMovement.PlayBounce();
            }
        }
        
    }

    void HandleTrigger(Collider2D col)
    {
        Debug.Log("tRIGGER");
    }
}
