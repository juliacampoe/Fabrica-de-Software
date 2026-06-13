using UnityEngine;

public class CameraFlow : MonoBehaviour
{
    public Transform player;

    [SerializeField] private float minX = 0f;
    [SerializeField] private float maxX = 50f;
    [SerializeField] private float fixedY = 0f;

    void LateUpdate()
    {
        if (player == null) return;

        float targetX = Mathf.Clamp(player.position.x, minX, maxX);

        transform.position = new Vector3(
            targetX,
            fixedY,
            -10f
        );
    }
}