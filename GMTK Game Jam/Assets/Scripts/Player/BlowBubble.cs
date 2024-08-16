using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowBubble : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] Transform bubbleAnchor;
    [SerializeField] float maxBubbleRadius = 5;
    [SerializeField] float bubbleScaleRate = 1f;
    [SerializeField] float timeBeforeShake = 1f;
    [SerializeField] float vibrationIntensity = 0.1f;
    [SerializeField] float vibrationSpeed = 5f;

    [SerializeField] GameObject bubbleSprite;
    GameObject activeBubble;
    Vector2 bubblePos;


    float time;
    float size;
    bool blowing = false;


    [SerializeField] GameObject NormalBubble;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            if (!blowing)
            {
                activeBubble = Instantiate(bubbleSprite);
                activeBubble.transform.position = bubbleAnchor.position;
                activeBubble.transform.parent = bubbleAnchor;
                blowing = true;
            }
            size += bubbleScaleRate * Time.deltaTime;
            if (size > maxBubbleRadius)
            {
                size = maxBubbleRadius;
                time += Time.deltaTime;
            }
            activeBubble.transform.localScale = Vector3.one* size;
            bubblePos = activeBubble.transform.GetChild(0).position;
            if (time > timeBeforeShake)
            {
                activeBubble.transform.localPosition = vibrationIntensity * new Vector3(
                    Mathf.PerlinNoise(vibrationSpeed * Time.time, 1),
                    Mathf.PerlinNoise(vibrationSpeed * Time.time, 2),
                    Mathf.PerlinNoise(vibrationSpeed * Time.time, 3));
            }
        }
        else
        {
            //Shoot Bubble
            if (activeBubble != null)
            {
                Destroy(activeBubble);
            }
            if (blowing)
            {
                ShootBubble(size);
            }
            blowing = false;
            size = 0;
            time = 0;
        }
    }

    void ShootBubble(float size)
    {
        GameObject bubble = Instantiate(NormalBubble);
        bubble.transform.localScale = Vector3.one * size;
        bubble.transform.position = bubblePos;
        bubble.GetComponent<BubbleMovement>().SetupBubble(playerMovement.GetLookDirection());

    }
}
