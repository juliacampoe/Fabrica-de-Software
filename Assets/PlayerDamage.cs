using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    [SerializeField] private int damage = 10;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerHealth playerHealth =
            collision.gameObject.GetComponent<PlayerHealth>();

        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }
    }
}