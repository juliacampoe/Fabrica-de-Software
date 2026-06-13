using UnityEngine;

public class Portal : MonoBehaviour
{
    private bool showText = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            showText = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            showText = false;
        }
    }

    void OnGUI()
    {
        if (showText)
        {
            GUI.Box(
                new Rect(
                    Screen.width / 2 - 100,
                    50,
                    200,
                    40
                ),
                "Fase 2 em breve 🚧"
            );
        }
    }
}