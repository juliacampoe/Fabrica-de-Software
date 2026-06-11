using UnityEngine;

public class CameraFlow : MonoBehaviour
{
    public Transform player;

    [SerializeField] private float minX = 0f;
    [SerializeField] private float maxX = 100f;

    [SerializeField] private float fixedY = 0f;

    void Update()
    {
        if (player == null)
        {
            return;
        }

        float cameraX = Mathf.Clamp(
            player.position.x,
            minX,
            maxX
        );

        transform.position = new Vector3(
            cameraX,
            fixedY,
            -10
        );
    }
}