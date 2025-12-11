

using UnityEngine;
using UnityEngine.InputSystem;

public class NewPlayerMovement : MonoBehaviour
{
    [SerializeField] private float velocidad = 5;
    [SerializeField] private float fuerzaSalto = 5;
    public Transform transformDetectorSuelo;
    public LayerMask layerSuelo;
    public float radioDeteccion;

    private Vector2 controlVector;
    private Rigidbody fisicas;
    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    void Start()
    {   
        fisicas = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        controlVector = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            animator.SetBool("enSuelo", false);
            fisicas.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse); 
        }
    }
    
    // Update is called once per frame
    void Update()
    {   
        Vector3 movimientoPersonaje = new Vector3(controlVector.x , 0, controlVector.y);
        transform.Translate(movimientoPersonaje * Time.deltaTime * velocidad);
        animator.SetFloat("Y", controlVector.y);
        animator.SetFloat("X", controlVector.x);
        var detectaSuelo = Physics.CheckSphere(transformDetectorSuelo.position , radioDeteccion, layerSuelo);
        if (detectaSuelo == true)
{
	animator.SetBool("enSuelo", true);
}
else
{
	animator.SetBool("enSuelo", false);
}
    }
    void OnDrawGizmosSelected()
{
	Gizmos.color = Color.blue;
	Gizmos.DrawWireSphere(transformDetectorSuelo.position, radioDeteccion);
}


    
}