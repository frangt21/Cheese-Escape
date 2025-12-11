using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // El objetivo que la cámara debe seguir (nuestro jugador).
    // Lo asignaremos desde el Inspector.
    [SerializeField] private Transform playerTarget;

    // Velocidad de suavizado del movimiento de la cámara.
    // Un valor más pequeño hará que la cámara siga más rápido.
    [SerializeField] private float smoothSpeed = 0.125f;

    // La distancia y ángulo fijos entre la cámara y el jugador.
    private Vector3 offset;

    // Start se llama antes del primer frame.
    void Start()
    {
        // Calculamos el "offset" inicial.
        // Es la diferencia de posición entre la cámara y el jugador al empezar el juego.
        // Esto nos permite colocar la cámara como queramos en el editor, y mantendrá esa perspectiva.
        offset = transform.position - playerTarget.position;
    }

    // LateUpdate se llama en cada frame, pero DESPUÉS de que se hayan ejecutado todos los Update().
    // Es el mejor lugar para el código de la cámara, para asegurarnos de que el jugador ya se ha movido.
    void LateUpdate()
    {
        // La posición donde la cámara DESEARÍA estar.
        Vector3 desiredPosition = playerTarget.position + offset;

        // Suavizamos la transición desde la posición actual de la cámara a la posición deseada.
        // Vector3.Lerp crea una interpolación lineal entre dos puntos.
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Aplicamos la nueva posición a la cámara.
        transform.position = smoothedPosition;
    }
}