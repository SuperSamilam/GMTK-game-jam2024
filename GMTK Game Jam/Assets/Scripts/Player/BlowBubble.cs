using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowBubble : MonoBehaviour
{
    [Header("Bubble")]
    [SerializeField] Transform bubbleAnchor; //Where the bubble spawn
    [SerializeField] float minRadius;
    [SerializeField] float maxRadius;
    [SerializeField] float scaleMultiplier;
    float radius;

    [SerializeField] GameObject bubblePrefab;
    GameObject activeBubble;

    [Header("Sound")]
    [SerializeField] AudioSource audioSource;

    [Header("Gum Types")]
    [SerializeField] GameObject normalBubble;

    bool blowing = false;

    [Header("Scripts")]
    [SerializeField] PlayerMovement playerMovement;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            //Not blowing start blowing the bubble
            if (!blowing)
            {
                activeBubble = Instantiate(bubblePrefab);
                activeBubble.transform.position = bubbleAnchor.transform.position;
                activeBubble.transform.parent = bubbleAnchor.transform;
                audioSource.Play();
            }

            //Increase the radius if it still possible to do so
            if (radius < maxRadius)
            {
                radius += scaleMultiplier * Time.deltaTime;
                activeBubble.transform.localScale = Vector3.one * radius;
            }
            else
            {
                audioSource.Stop();
            }

            blowing = true;
        }
        else
        {
            //Bubble has just been blown
            if (blowing)
            {
                audioSource.Stop();
                //Allowed to shoot
                if (radius > minRadius)
                {
                    ShootBubble(radius);
                }
                Destroy(activeBubble);
                blowing = false;
                radius = 0;
            }
             
        }
    }

    void ShootBubble(float size)
    {
        GameObject bubble = Instantiate(normalBubble);
        bubble.transform.localScale = Vector3.one * size;
        bubble.transform.position = activeBubble.transform.GetChild(0).transform.position;
        bubble.GetComponent<BubbleMovement>().SetupBubble(playerMovement.GetLookDirection(), size);

    }
}
