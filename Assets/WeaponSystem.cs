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

        Debug.Log("WEAPON SYSTEM INICIADO ✔");
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
            Debug.Log("TECLA R PRESSIONADA → RECARREGANDO");

            StartCoroutine(
                Reload()
            );
        }

        // TIRO
        if (Input.GetKeyDown(
            KeyCode.Alpha1))
        {
            Debug.Log("TIRO DISPARADO | Ammo: " + currentAmmo);

            playerController
                .PlayAttackAnimation();

            Shoot();
        }
    }

    void Shoot()
    {
        if (isReloading)
        {
            Debug.Log("TENTOU ATIRAR MAS ESTÁ RECARREGANDO");
            return;
        }

        if (currentAmmo <= 0)
        {
            Debug.Log("SEM MUNIÇÃO!");
            return;
        }

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

        Debug.Log("TIRO OK | Ammo restante: " + currentAmmo);
    }

    IEnumerator Reload()
    {
        isReloading = true;

        Debug.Log("RECARREGANDO...");

        yield return
            new WaitForSeconds(
                reloadTime
            );

        currentAmmo =
            maxAmmo;

        isReloading = false;

        Debug.Log("RECARREGADO COM SUCESSO ✔ Ammo: " + currentAmmo);
    }
}