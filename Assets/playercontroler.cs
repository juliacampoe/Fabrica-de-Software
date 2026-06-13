using UnityEngine;

public class playercontroler : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;

    private Rigidbody2D rb;
    private Animator animator;

    private float movementInput;
    private bool isGrounded;
    private bool facingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Movimento horizontal
        movementInput = Input.GetAxisRaw("Horizontal");

        // DIREITA
        if (movementInput > 0)
        {
            facingRight = true;
            animator.Play("player walk right");
        }

        // ESQUERDA
        else if (movementInput < 0)
        {
            facingRight = false;
            animator.Play("player walk left");
        }

        // IDLE
        else
        {
            if (facingRight)
            {
                animator.Play("player idle right");
            }
            else
            {
                animator.Play("player idle left");
            }
        }

        // PULO
        if (Input.GetKeyDown(KeyCode.Space)
            && isGrounded)
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                jumpForce
            );

            isGrounded = false;
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(
            movementInput * movementSpeed,
            rb.linearVelocity.y
        );
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    public void PlayAttackAnimation()
    {
        if (facingRight)
        {
            animator.Play("player right attack");
        }
        else
        {
            animator.Play("player left attack");
        }
    }

    public void PlayHurtAnimation()
    {
        if (facingRight)
        {
            animator.Play("player hurt right");
        }
        else
        {
            animator.Play("player hurt left");
        }
    }

    public void PlayDeathAnimation()
    {
        if (facingRight)
        {
            animator.Play("player death right");
        }
        else
        {
            animator.Play("player death left");
        }
    }
}