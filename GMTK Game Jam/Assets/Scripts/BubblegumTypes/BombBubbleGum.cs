using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBubbleGum : MonoBehaviour
{
    [SerializeField] BubbleMovement bubbleMovement;
    [SerializeField] AudioClip bombFizzing;

    private void Start()
    {
        bubbleMovement.BounceEvent += HandleBounce;
    }

    void Update()
    {

    }

    void HandleBounce(Collision2D col)
    {
        bubbleMovement.popWithAnimation();
        if (col.gameObject.tag == "BombWall")
        {
            Destroy(col.gameObject);
        }
    }



}
