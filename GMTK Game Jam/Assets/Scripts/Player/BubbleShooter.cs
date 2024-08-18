using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleShooter : MonoBehaviour
{
    [SerializeField] Transform bubbleSpawn;
    [SerializeField] GameObject bubbleExpandable;
    [SerializeField] GameObject bubblePrefab;
    [SerializeField] PlayerMovement playerMovement;

    [SerializeField] float minRadius = 0.4f;
    [SerializeField] float maxRadius = 1.5f;
    [SerializeField] float growthScale = 1.0f;
    
    float radius;
    bool blowing = false;
    GameObject activeBubble;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (!blowing)
            {
                radius = 0;
                blowing = true;
                playerMovement.blowing = true;
                activeBubble = Instantiate(bubbleExpandable);
                activeBubble.transform.position = bubbleSpawn.position;
                activeBubble.transform.parent = transform;
            }

            if (radius < maxRadius)
            {
                radius += growthScale * Time.deltaTime;
                activeBubble.transform.localScale = Vector3.one * radius;
            }
        }
        else //stopped blowing or wasent blowing
        {
            if (blowing)//Just stopped blowing
            {
                if (radius > minRadius)//Big Enoguth shoot
                {
                    SpawnBubble();
                }

                Destroy(activeBubble);
                blowing = false;
                playerMovement.blowing = false;
            }
        }
    }

    void SpawnBubble()
    {
        var b = Instantiate(bubblePrefab);
        b.transform.position = activeBubble.transform.GetChild(0).transform.position;
        Vector2 dir = -transform.right;
        if (playerMovement.isFacingRight)
        {
            dir = transform.right;
        }
        b.GetComponent<Bubble>().Setup(radius, dir);
    }
}
