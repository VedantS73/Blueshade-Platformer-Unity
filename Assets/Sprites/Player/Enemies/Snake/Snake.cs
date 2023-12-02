using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : Enemy
{
    [SerializeField] private float leftCap;
    [SerializeField] private float rightCap;
    [SerializeField] private float patrolSpeed = 1.3f;

    private bool facingRight = true;

    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (facingRight)
        {
            if (transform.position.x < rightCap)
            {
                if (transform.localScale.x != 1)
                {
                    transform.localScale = new Vector3(1, 1);
                }
                rb.velocity = new Vector2(patrolSpeed, rb.velocity.y);
            }
            else
            {
                facingRight = false;
            }
        }
        else
        {
            if (transform.position.x > leftCap)
            {
                if (transform.localScale.x != -1)
                {
                    transform.localScale = new Vector3(-1, 1);
                }
                rb.velocity = new Vector2(-patrolSpeed, rb.velocity.y);
            }
            else
            {
                facingRight = true;
            }
        }
    }
    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            if (facingRight)
                collision.transform.gameObject.GetComponent<PlayerMovement>().TakeDamage(2, 8f);
            else
                collision.transform.gameObject.GetComponent<PlayerMovement>().TakeDamage(2, -8f);
        }
    }*/
}
