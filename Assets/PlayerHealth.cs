using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;

    private int currentHealth;
    private bool isDead = false;

    private Rigidbody2D rb;
    private playercontroler playerController;

    void Start()
    {
        currentHealth = maxHealth;

        rb = GetComponent<Rigidbody2D>();
        playerController = GetComponent<playercontroler>();

        Debug.Log("PLAYER HEALTH INICIADO ✔ Vida: " + currentHealth);
    }

    public void TakeDamage(int damage)
    {
        if (isDead)
            return;

        currentHealth -= damage;

        // 🔥 DEBUG BONITO
        Debug.Log(
            $"💔 Dano: {damage} | ❤️ Vida: {currentHealth}"
        );

        playerController.PlayHurtAnimation();

        if (currentHealth <= 0)
        {
            Debug.Log("💀 PLAYER MORREU");

            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        isDead = true;

        rb.linearVelocity = Vector2.zero;
        rb.simulated = false;

        playerController.PlayDeathAnimation();

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("GameOver");
    }
}