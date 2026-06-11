using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float stopDistance = 1.2f;
    [SerializeField] private float chaseDistance = 8f;

    private Transform player;
    private Rigidbody2D rb;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (player == null)
            return;

        float distance = Vector2.Distance(
            transform.position,
            player.position
        );

        // Muito longe → não segue
        if (distance > chaseDistance)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        // Muito perto → para de andar
        if (distance <= stopDistance)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        float direction =
            player.position.x > transform.position.x
            ? 1f
            : -1f;

        rb.linearVelocity =
            new Vector2(direction * speed, 0);
    }
}