using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float stopDistance = 1.2f;
    [SerializeField] private float chaseDistance = 8f;

    private Transform player;
    private Rigidbody2D rb;
    private Animator animator;

    private bool facingRight = true;
    private bool isDead = false;

    void Start()
    {
        GameObject playerObject =
            GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
            player = playerObject.transform;

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        Debug.Log("ENEMY INICIADO ✔");
    }

    void FixedUpdate()
    {
        if (player == null || isDead)
            return;

        float distance =
            Vector2.Distance(transform.position, player.position);

        // longe
        if (distance > chaseDistance)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            return;
        }

        // ataque
        if (distance <= stopDistance)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);

            animator.Play(
                facingRight
                    ? "enemy right attack"
                    : "enemy left attack"
            );

            return;
        }

        // movimento
        float direction =
            player.position.x > transform.position.x ? 1f : -1f;

        rb.linearVelocity =
            new Vector2(direction * speed, rb.linearVelocity.y);

        // animação
        if (direction > 0)
        {
            facingRight = true;
            animator.Play("enemy right walking");
        }
        else
        {
            facingRight = false;
            animator.Play("enemy left walking");
        }
    }

    public void StopEnemy()
    {
        isDead = true;
        rb.linearVelocity = Vector2.zero;
    }
}