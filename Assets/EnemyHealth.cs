using System.Collections;
using UnityEngine;

public class EnemyHealth
    : MonoBehaviour
{
    [SerializeField]
    private int health = 3;

    private bool isDead
        = false;

    private EnemyAI enemyAI;
    private Animator animator;
    private Rigidbody2D rb;

    void Start()
    {
        enemyAI =
            GetComponent
            <EnemyAI>();

        animator =
            GetComponent
            <Animator>();

        rb =
            GetComponent
            <Rigidbody2D>();
    }

    public void TakeDamage(
        int damage)
    {
        if (isDead)
            return;

        health -= damage;

        if (health <= 0)
        {
            StartCoroutine(
                Die()
            );
        }
    }

    IEnumerator Die()
    {
        isDead = true;

        enemyAI.StopEnemy();

        rb.simulated =
            false;

        animator.Play(
            "enemy dying"
        );

        yield return
            new WaitForSeconds(
                1f
            );

        Destroy(gameObject);
    }
}