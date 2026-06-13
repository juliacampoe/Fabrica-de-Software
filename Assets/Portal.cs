using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private bool playerNear;
    private bool transitioning;

    void Update()
    {
        if (playerNear && !transitioning)
        {
            animator.Play("portal animation");

            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(EnterPortal());
            }
        }
    }

    System.Collections.IEnumerator EnterPortal()
    {
        transitioning = true;

        Debug.Log("Entrando no portal...");

        animator.Play("portal animation");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("FASE 2");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        playerNear = true;

        Debug.Log("Player no portal");
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        playerNear = false;
    }
}