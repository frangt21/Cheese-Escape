using UnityEngine;

public class Collectionable : MonoBehaviour
{
	public void OnTriggerEnter(Collider other)
    {
	if (other.CompareTag("Fruta"))
	{
	Destroy(other.gameObject);
	}
 }
}