using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy
{
    [SerializeField] private float leftCap;
    [SerializeField] private float rightCap;

    [SerializeField] private float patrolSpeed = 1.3f;
    [SerializeField] private float rammingSpeed = 4f;

    private bool facingRight = true;
    private RaycastHit2D hit;
    private int rayCastLength = 6;
    public Transform raypoint;
    private float rlm = 1f;
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (facingRight)
            rlm = 1f;
        else
            rlm = -1f;
        hit = Physics2D.Raycast(raypoint.position, Vector2.right * rlm, rayCastLength);
        Debug.DrawRay(raypoint.position, Vector2.right * rayCastLength, Color.red);
        anim.SetBool("AttackMode", false);
        if (hit && hit.transform.tag == "Player")
        {
            if(Vector2.Distance(hit.transform.position,transform.position) <= 1.2f)
            {
                anim.SetBool("AttackMode", true);
                rb.velocity = Vector2.zero;
            }
            else
                rb.velocity = new Vector2(rammingSpeed * rlm, rb.velocity.y);
        }
        else
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
    }
    void AttackPlayer()
    {
        hit = Physics2D.Raycast(raypoint.position, Vector2.right, 1.2f);
        if (hit.transform.tag == "Player")
        {
            anim.SetBool("AttackMode", false);
            if(facingRight)
                hit.transform.gameObject.GetComponent<PlayerMovement>().TakeDamage(2,8f,5f);
            else
                hit.transform.gameObject.GetComponent<PlayerMovement>().TakeDamage(2,-8f,5f);
        }
    }
}
