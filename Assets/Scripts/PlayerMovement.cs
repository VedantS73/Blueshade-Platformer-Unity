using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D col;
    public Transform GroundCheckPt;
    public float GroundCheckRad;
    public bool isGrounded = false;
    public LayerMask GroundLayer;
    [SerializeField]private AudioSource footstep;

    public int coins = 0;
    public TextMeshProUGUI cointxt;

    private float horizontal;
    private float speed = 6f;
    private float jumpingPower = 15f;
    public bool isFacingRight = true;

    public Joystick joystick;
    public FixedButton jumpBtn;

    private enum State {idle, running, jumping, falling, hurt};
    [SerializeField] private State state = State.idle;

    [SerializeField]
    private Animator anim;
    [SerializeField]
    private int health;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        footstep = GetComponent<AudioSource>();
        cointxt.SetText("Coins : " + coins);
    }

    void Update()
    {
        if(Physics2D.BoxCast(col.bounds.center, col.bounds.size, 0f, Vector2.down, 1f, GroundLayer).collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        if(state != State.hurt)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
        if (joystick.Horizontal >= .4f)
        {
            horizontal = 1;
        }
        else if(joystick.Horizontal <= -.4f)
        {
            horizontal = -1;
        }
        else
        {
            horizontal = 0;
        }

        //if(jumpBtn.Pressed && col.IsTouchingLayers(GroundLayer))
        if (jumpBtn.Pressed && isGrounded)
        {
            Jump();
        }

        if(!isFacingRight && horizontal > 0f)
        {
            Flip();
        }
        else if (isFacingRight && horizontal < 0f)
        {
            Flip();
        }
        VelocityState();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Collectable")
        {
            Destroy(collision.gameObject);
            coins++;
            cointxt.SetText("Coins : " + coins);
        }
        else if (collision.tag == "Norb")
        {
            Debug.Log("DUNOO REACHED ITT");
            Animator orbanim = collision.gameObject.GetComponent<Animator>();
            orbanim.SetTrigger("nextlevel");
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (state == State.falling)
            {
                enemy.JumpedOn();
                Jump();
            }
            else
            {
                if (collision.gameObject.GetComponent<Snake>() != null)
                {
                    //state = State.hurt;
                    if (collision.gameObject.transform.position.x > transform.position.x)
                    {
                        TakeDamage(1, -8f);
                        //rb.velocity = new Vector2(-8f, rb.velocity.y);
                    }
                    else
                    {
                        TakeDamage(1, 8f);
                        //rb.velocity = new Vector2(8f, rb.velocity.y);
                    }
                }
            }
        }
        else if(collision.gameObject.tag == "Spikes")
        {
            TakeDamage(4, 0, 9f);
        }
        
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && col.IsTouchingLayers(GroundLayer))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }
        state = State.jumping;
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        state = State.jumping;
    }

    private void VelocityState()
    {
        if(state == State.jumping)
        {
            if(rb.velocity.y < .1f)
            {
                state = State.falling;
            }
        }
        else if(state == State.falling)
        {
            if (col.IsTouchingLayers(GroundLayer))
            {
                state = State.idle;
            }
        }
        else if(state == State.hurt)
        {
            if(Mathf.Abs(rb.velocity.x) < .1f)
            {
                state = State.idle;
            }
        }
        else if(Mathf.Abs(rb.velocity.x) > Mathf.Epsilon)
        {
            state = State.running;
        }
        else
        {
            state = State.idle;
        }
        anim.SetInteger("state", (int)state);
    }

    private void Footsteps()
    {
        footstep.Play();
    }

    public void TakeDamage(int damage, float impulse_x, float impulse_y=0)
    {
        if (health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            health -= damage;
        }
        state = State.hurt;
        rb.AddForce(new Vector2(impulse_x, rb.velocity.y+impulse_y), ForceMode2D.Impulse);
        //rb.velocity = new Vector2(impulse_x, rb.velocity.y+impulse_y);
        //Debug.Log("Player registered damage : " + damage + " and impulse : " + impulse_x);
    }
}
