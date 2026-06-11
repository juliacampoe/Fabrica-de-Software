using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    [SerializeField] private float damageCooldown = 1f;

    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        PlayerHealth playerHealth =
            collision.gameObject.GetComponent<PlayerHealth>();

        if (playerHealth != null
            && timer >= damageCooldown)
        {
            playerHealth.TakeDamage(damage);

            timer = 0f;
        }
    }
}