using System.Collections;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float bulletSpeed = 10f;

    [Header("Ammo")]
    [SerializeField] private int maxAmmo = 10;
    [SerializeField] private float reloadTime = 2f;

    private int currentAmmo;
    private bool isReloading;
    private bool facingRight = true;

    void Start()
    {
        currentAmmo = maxAmmo;
    }

    void Update()
    {
        // Direção do player
        float moveInput = Input.GetAxisRaw("Horizontal");

        if (moveInput > 0)
            facingRight = true;
        else if (moveInput < 0)
            facingRight = false;

        // Recarregar
        if (Input.GetKeyDown(KeyCode.R)
            && !isReloading
            && currentAmmo < maxAmmo)
        {
            StartCoroutine(Reload());
        }

        // Atirar
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (isReloading)
        {
            Debug.Log("Recarregando...");
            return;
        }

        if (currentAmmo <= 0)
        {
            Debug.Log("Sem munição! Aperte R");
            return;
        }

        currentAmmo--;

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

        Debug.Log("Munição restante: " + currentAmmo);
    }

    IEnumerator Reload()
    {
        isReloading = true;

        Debug.Log("Recarregando...");

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
        isReloading = false;

        Debug.Log("Recarga concluída!");
    }
}