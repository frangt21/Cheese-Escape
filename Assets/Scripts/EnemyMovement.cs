using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
	[SerializeField] private LayerMask playerLayer;
	[SerializeField] private Transform playerTransform;
	[SerializeField] private float rangoDeteccion;
	[SerializeField] private float velocidad = 4f;
    void Start()
    {
        
    }

    void Update()
    {
        var playerDetectado = Physics.CheckSphere(transform.position, rangoDeteccion, playerLayer);
	if (playerDetectado == true)
	{
	transform.LookAt(playerTransform);
    Vector3 posicionPlayerSinY = new Vector3(playerTransform.position.x,1,playerTransform.position.z);
    Vector3 movimiento = Vector3.MoveTowards(transform.position, posicionPlayerSinY, velocidad * Time.deltaTime);
    transform.position = movimiento;
}
    }

	public void OnDrawGizmos()
{
	Gizmos.color = Color.red;
	Gizmos.DrawWireSphere(transform.position, rangoDeteccion);
}
}
