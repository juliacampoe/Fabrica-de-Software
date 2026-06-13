using System.Collections;
using UnityEngine;

public class WeaponSystem
    : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private Transform firePoint;

    [SerializeField]
    private float bulletSpeed = 10f;

    [Header("Ammo")]
    [SerializeField]
    private int maxAmmo = 10;

    [SerializeField]
    private float reloadTime = 2f;

    private int currentAmmo;
    private bool isReloading;
    private bool facingRight = true;

    private playercontroler
        playerController;

    void Start()
    {
        currentAmmo = maxAmmo;

        playerController =
            GetComponent
            <playercontroler>();
    }

    void Update()
    {
        float moveInput =
            Input.GetAxisRaw(
                "Horizontal"
            );

        if (moveInput > 0)
            facingRight = true;
        else if (moveInput < 0)
            facingRight = false;

        // RELOAD
        if (Input.GetKeyDown(
            KeyCode.R)
            && !isReloading
            && currentAmmo
            < maxAmmo)
        {
            StartCoroutine(
                Reload()
            );
        }

        // TIRO
        if (Input.GetKeyDown(
            KeyCode.Alpha1))
        {
            playerController
                .PlayAttackAnimation();

            Shoot();
        }
    }

    void Shoot()
    {
        if (isReloading)
            return;

        if (currentAmmo <= 0)
            return;

        currentAmmo--;

        GameObject bullet =
            Instantiate(
                bulletPrefab,
                firePoint.position,
                Quaternion.identity
            );

        Rigidbody2D rb =
            bullet.GetComponent
            <Rigidbody2D>();

        float direction =
            facingRight
            ? 1f
            : -1f;

        rb.linearVelocity =
            new Vector2(
                direction
                * bulletSpeed,
                0
            );
    }

    IEnumerator Reload()
    {
        isReloading = true;

        yield return
            new WaitForSeconds(
                reloadTime
            );

        currentAmmo =
            maxAmmo;

        isReloading = false;
    }
}