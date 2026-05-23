using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int damage = 1;

    void Start()
    {
        Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Encostou em: " + collision.name);

        EnemyHealth enemy = collision.GetComponent<EnemyHealth>();

        if (enemy != null)
        {
            Debug.Log("DEU DANO");

            enemy.TakeDamage(damage);

            Destroy(gameObject);
        }
    }
}