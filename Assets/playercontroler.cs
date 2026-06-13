using System.Collections;
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

    private bool isAttacking = false;
    private bool isDead = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        Debug.Log("PLAYER INICIADO ✔");
    }

    void Update()
    {
        if (isDead)
            return;

        movementInput = Input.GetAxisRaw("Horizontal");

        // DIREITA
        if (movementInput > 0)
        {
            facingRight = true;

            if (!isAttacking)
            {
                animator.Play("player walk right");
            }
        }

        // ESQUERDA
        else if (movementInput < 0)
        {
            facingRight = false;

            if (!isAttacking)
            {
                animator.Play("player walk left");
            }
        }

        // IDLE
        else
        {
            if (!isAttacking)
            {
                animator.Play(
                    facingRight
                    ? "player idle right"
                    : "player idle left"
                );
            }
        }

        // PULO
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Debug.Log("PULO EXECUTADO");

            rb.linearVelocity =
                new Vector2(rb.linearVelocity.x, jumpForce);

            isGrounded = false;
        }
    }

    void FixedUpdate()
    {
        if (isDead)
            return;

        rb.linearVelocity =
            new Vector2(
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
        if (isAttacking || isDead)
            return;

        Debug.Log("ATAQUE");

        StartCoroutine(AttackAnimation());
    }

    IEnumerator AttackAnimation()
    {
        isAttacking = true;

        animator.Play(
            facingRight
            ? "player right attack"
            : "player left attack"
        );

        yield return new WaitForSeconds(0.3f);

        isAttacking = false;
    }

    public void PlayHurtAnimation()
    {
        if (isDead)
            return;

        Debug.Log("TOMOU DANO");

        animator.Play(
            facingRight
            ? "player hurt right"
            : "player hurt left"
        );
    }

    public void PlayDeathAnimation()
    {
        isDead = true;

        Debug.Log("PLAYER MORREU");

        animator.Play(
            facingRight
            ? "player death right"
            : "player death left"
        );
    }
}