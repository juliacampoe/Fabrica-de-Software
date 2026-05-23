using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float bulletSpeed = 10f;

    private bool facingRight = true;

    void Update()
    {
        // Detecta direção do player
        float moveInput = Input.GetAxisRaw("Horizontal");

        if (moveInput > 0)
        {
            facingRight = true;
        }
        else if (moveInput < 0)
        {
            facingRight = false;
        }

        // Atira ao apertar 1
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(
            bulletPrefab,
            firePoint.position,
            Quaternion.identity
        );

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        float direction = facingRight ? 1f : -1f;

        rb.linearVelocity = new Vector2(
            direction * bulletSpeed,
            0
        );
    }
}