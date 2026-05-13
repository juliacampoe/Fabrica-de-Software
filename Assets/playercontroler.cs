using UnityEngine;
using UnityEngine.InputSystem;

public class playercontroler : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;

    private Rigidbody2D rb;
    private float movementInput;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Movimento
        movementInput = 0;

        if (Keyboard.current.aKey.isPressed)
        {
            movementInput = -1;
        }

        if (Keyboard.current.dKey.isPressed)
        {
            movementInput = 1;
        }

        // Pulo
        if (Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                jumpForce
            );

            isGrounded = false;
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(
            movementInput * movementSpeed,
            rb.linearVelocity.y
        );
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}