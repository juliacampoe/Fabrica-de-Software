using UnityEngine;

public class CameraFlow : MonoBehaviour
{
    public Transform player;

    [SerializeField] private float minX = -8.54f;
    [SerializeField] private float maxX = 213.76f;
    [SerializeField] private float fixedY = 0f;

    void LateUpdate()
    {
        if (player == null)
        {
            Debug.Log("PLAYER NÃO ATRIBUÍDO NA CÂMERA");
            return;
        }

        float x = Mathf.Clamp(player.position.x, minX, maxX);

        transform.position = new Vector3(x, fixedY, -10f);
    }
}