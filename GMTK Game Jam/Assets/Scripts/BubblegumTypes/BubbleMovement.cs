using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static UnityEditor.Searcher.SearcherWindow.Alignment;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.XR;

public class BubbleMovement : MonoBehaviour
{
    public Vector2 direction;
    [SerializeField] public float speed;
    [SerializeField] float imunityTime = 1f;


    [SerializeField] Animator animator;

    [Header("Audio")]
    [SerializeField] public AudioSource audioSource;
    [SerializeField] AudioClip popSound;
    [SerializeField] AudioClip bounceSound;
    [SerializeField] AudioClip travelSound;

    public Rigidbody2D rb;
    CircleCollider2D col;
    float time = 0;
    
    public float bounces;


    public event Action<Collision2D> BounceEvent;
    public event Action<Collider2D> TriggerEvent;
    public bool ignoreReflection = false;
    public bool addForece = true;

    //Setup classe
    private void Start()
    {
        //Sets up the ball and makes it so it wont kill the player
        col = GetComponent<CircleCollider2D>();
        col.enabled = false;
    }
    public void SetupBubble(Vector2 dir, float size)
    {
        direction = dir;
        rb.mass = size;
    }


    void Update()
    {
        if (time < imunityTime)
        {
            time += Time.deltaTime;
        }
        else
        {
            col.enabled = true; 
        }

        if (addForece)
        {
            rb.velocity = new Vector2(direction.x * speed, direction.y * speed);
        }
    }
    //Audio
    public void PlayPop()
    {
        audioSource.loop = false;
        audioSource.clip = popSound;
        audioSource.Play();
    }

    public void PlayBounce()
    {
        audioSource.loop = false;
        audioSource.clip = bounceSound;
        audioSource.Play();
    }

    public void PlayTraveling()
    {
        audioSource.loop = true;
        audioSource.clip = travelSound;
        audioSource.Play();
    }


    //Pop the ballon
    public void pop()
    {
        Destroy(this.gameObject);
    }

    public void popWithAnimation(float animationSpeed = 1)
    {
        speed = 0;
        animator.SetTrigger("pop");
        animator.speed = animationSpeed;
        PlayPop();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        BounceEvent?.Invoke(collision);
        if (!ignoreReflection)
        {
            direction = Vector2.Reflect(direction, collision.contacts[0].normal);
        }
        bounces++;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        TriggerEvent?.Invoke(collision);
    }

}
