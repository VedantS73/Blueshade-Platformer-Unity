using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    Rigidbody2D rb;
    CircleCollider2D bcol;
    public bool gravityCoin = true;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bcol = GetComponent<CircleCollider2D>();
        if (!gravityCoin)
        {
            rb.isKinematic = true;
            bcol.isTrigger = true;
        }
    }

    private void Update()
    {
        /*if (!gravityCoin)
        {
            transform.position = Vector3.up * Mathf.Cos(Time.time);
        }*/
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            rb.isKinematic = true;
            bcol.isTrigger = true;
        }
    }
}
