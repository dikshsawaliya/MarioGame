using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingGoomba : Enemy 
{
    public float leftCap = 14.5f;
    public float rightCap = 45.32f;

    public float jumpLength = 2f;
    public float jumpHeight = 2f;
    public LayerMask ground;
    private Collider2D coll;
    private Rigidbody2D rb;
    private Animator anim;

    private bool facingLeft = true;

    protected override void Start()

    {
        base.Start();
        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (facingLeft)
        {
            if (transform.position.x > leftCap)
            {
                if (transform.localScale.x != 1)
                {
                    transform.localScale = new Vector3(5, 5);
                }

                if (coll.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(-jumpLength, jumpHeight);
                }
            }
            else
            {
                facingLeft = false;
            }
        }

        else
        {
            if (transform.position.x < rightCap)
            {
                if (transform.localScale.x != -1)
                {
                    transform.localScale = new Vector3(-5, 5);
                }

                if (coll.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(jumpLength, jumpHeight);
                }
            }
            else
            {
                facingLeft = true;
            }
        }
    }

}

	