using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float stopDistance = 1.2f;
    [SerializeField] private float chaseDistance = 8f;
    [SerializeField] private float jumpForce = 7f;

    private Transform player;
    private Rigidbody2D rb;
    private Animator animator;

    private bool facingRight = true;
    private bool isDead = false;
    private bool isGrounded;

    void Start()
    {
        GameObject playerObject =
            GameObject.FindGameObjectWithTag(
                "Player"
            );

        if (playerObject != null)
        {
            player =
                playerObject.transform;
        }

        rb = GetComponent<Rigidbody2D>();
        animator =
            GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (player == null || isDead)
            return;

        float distance =
            Vector2.Distance(
                transform.position,
                player.position
            );

        // Muito longe
        if (distance > chaseDistance)
        {
            rb.linearVelocity =
                new Vector2(
                    0,
                    rb.linearVelocity.y
                );

            return;
        }

        // Perto -> atacar
        if (distance <= stopDistance)
        {
            rb.linearVelocity =
                new Vector2(
                    0,
                    rb.linearVelocity.y
                );

            animator.Play(
                facingRight
                ? "enemy right attack"
                : "enemy left attack"
            );

            return;
        }

        // Seguir player
        float direction =
            player.position.x >
            transform.position.x
            ? 1f
            : -1f;

        rb.linearVelocity =
            new Vector2(
                direction * speed,
                rb.linearVelocity.y
            );

        // Verifica obstáculo
        Vector2 rayOrigin =
            new Vector2(
                transform.position.x,
                transform.position.y - 0.2f
    );

        RaycastHit2D wallHit =
            Physics2D.Raycast(
                rayOrigin,
                Vector2.right * direction,
                1.2f
    );

        bool playerHigher =
            player.position.y >
            transform.position.y
            + 0.5f;

        // Pulo
        if (
            isGrounded &&
            (
                playerHigher
                || wallHit.collider
                != null
            )
        )
        {
            rb.linearVelocity =
                new Vector2(
                    rb.linearVelocity.x,
                    jumpForce
                );

            isGrounded =
                false;
        }

        // Walk animation
        if (direction > 0)
        {
            facingRight = true;

            animator.Play(
                "enemy right walking"
            );
        }
        else
        {
            facingRight = false;

            animator.Play(
                "enemy left walking"
            );
        }
    }

    private void
    OnCollisionEnter2D(
        Collision2D collision
    )
    {
        if (
            collision.gameObject
            .CompareTag(
                "Ground"
            )
        )
        {
            isGrounded = true;
        }
    }

    public void StopEnemy()
    {
        isDead = true;

        rb.linearVelocity =
            Vector2.zero;
    }
}