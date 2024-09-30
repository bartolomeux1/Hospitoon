using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // O jogador que a câmera deve seguir
    public Vector3 offset; // Offset da câmera em relação ao jogador
    public float smoothSpeed = 0.125f; // A velocidade de suavização do movimento da câmera

    // BoxCollider2D para limites
    public BoxCollider2D boundary; // Arraste o collider para essa variável no Inspector

    void LateUpdate()
    {
        if (target != null && boundary != null)
        {
            // Define a posição desejada da câmera, mantendo a mesma profundidade
            Vector3 desiredPosition = new Vector3(target.position.x, target.position.y, transform.position.z) + offset;

            // Restringe a posição desejada dentro dos limites
            desiredPosition.x = Mathf.Clamp(desiredPosition.x, boundary.bounds.min.x, boundary.bounds.max.x);
            desiredPosition.y = Mathf.Clamp(desiredPosition.y, boundary.bounds.min.y, boundary.bounds.max.y);

            // Move a câmera suavemente para a posição desejada
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
