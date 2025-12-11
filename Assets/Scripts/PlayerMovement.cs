

using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float velocidad = 0;
    [SerializeField] private float fuerzaSalto = 0;
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
            fisicas.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse); 
            animator.SetBool("enSuelo", false);
        }
    }
    
    // Update is called once per frame
    void Update()
    {   
        Vector3 movimientoPersonaje = new Vector3(controlVector.x , 0, controlVector.y);
        transform.Translate(movimientoPersonaje * Time.deltaTime * velocidad);
        animator.SetFloat("Y", controlVector.y);
        animator.SetFloat("X", controlVector.x);
    }
    private void OnCollisionEnter(Collision collision)
{
	if (collision.gameObject.CompareTag("Suelo") == true)
{
	animator.SetBool("enSuelo", true);
}
}
}

    
