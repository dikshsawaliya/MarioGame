using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    public LayerMask ground;
    private float speed = 5f;
    private float JumpForce = 10f;
    private int coins = 0;
    public Text coinsText;
    private float hurtForce = 5f;

    public AudioSource coinsound;
    public AudioSource footsteps;
    public int health;
    public Text healthAmount;

    private enum State { inactive, sprinting, jumping, descend, injure }
    private State state = State.inactive;
    private Collider2D coll;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        healthAmount.text = health.ToString();
    }

    private void Update()
    {
        if (state != State.injure)
        {
            Movement();
        }
        AnimationState();
        anim.SetInteger("state", (int)state);
    }

    private void Movement()
    {
        Vector3 old_pos = transform.position;

        if (Input.GetKey(KeyCode.D))
        {
            Vector3 newpos = new Vector3(old_pos.x + speed * Time.deltaTime, old_pos.y, old_pos.z );
            transform.position = newpos;
            transform.localScale = new Vector2(7, 7);
        }

        if (Input.GetKey(KeyCode.A))
        {
            Vector3 newpos = new Vector3(old_pos.x - speed * Time.deltaTime, old_pos.y, old_pos.z);
            transform.position = newpos;
            transform.localScale = new Vector2(-7, 7);
        }


        //jumping
        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
        {
            Jump();
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, JumpForce);
        state = State.jumping;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Collectable")
        {
            coinsound.Play();
            Destroy(collision.gameObject);
            coins += 1;
            coinsText.text = coins.ToString();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if (state == State.descend)
            {
                enemy.JumpedOn();
                Jump();
            }
            else
            {
                state = State.injure;
                HandleHealth();

                if (other.gameObject.transform.position.x > transform.position.x)
                {
                    rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(hurtForce, rb.velocity.y);
                }
            }
        }
    }

    private void HandleHealth()
    {
        health -= 1;
        healthAmount.text = health.ToString();
        if (health <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }


    private void AnimationState()
    {

        if (state == State.jumping)
        {
            if (rb.velocity.y < 0.1f)
            {
                state = State.descend;
            }
        }
        else if (state == State.descend)
        {
            if (coll.IsTouchingLayers(ground))
            {
                state = State.inactive;
            }
        }
        else if (state == State.injure)
        {
            if (Mathf.Abs(rb.velocity.x) < .1f)
            {
                state = State.inactive;
            }
        }

        else if (Input.GetKey(KeyCode.D))
        {
            state = State.sprinting;

        }

        else if (Input.GetKey(KeyCode.A))
        {
            state = State.sprinting;
        }

        else
        {
            state = State.inactive;

        }

       
    }

    private void Footsteps()
    {
        footsteps.Play();
    }
}