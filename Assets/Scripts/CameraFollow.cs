using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // O jogador que a c�mera deve seguir
    public Vector3 offset; // Offset da c�mera em rela��o ao jogador
    public float smoothSpeed = 0.125f; // A velocidade de suaviza��o do movimento da c�mera

    // BoxCollider2D para limites
    public BoxCollider2D boundary; // Arraste o collider para essa vari�vel no Inspector

    void LateUpdate()
    {
        if (target != null && boundary != null)
        {
            // Define a posi��o desejada da c�mera, mantendo a mesma profundidade
            Vector3 desiredPosition = new Vector3(target.position.x, target.position.y, transform.position.z) + offset;

            // Restringe a posi��o desejada dentro dos limites
            desiredPosition.x = Mathf.Clamp(desiredPosition.x, boundary.bounds.min.x, boundary.bounds.max.x);
            desiredPosition.y = Mathf.Clamp(desiredPosition.y, boundary.bounds.min.y, boundary.bounds.max.y);

            // Move a c�mera suavemente para a posi��o desejada
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
