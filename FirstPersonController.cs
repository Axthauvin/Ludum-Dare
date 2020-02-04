using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody))]
public class FirstPersonController : MonoBehaviour
{
    public float speed = 1f;
    public float StepInterval = 5f;
    public float detente = 5f;

    
    public bool isGrounded;
    Rigidbody rb_;

    // Start is called before the first frame update
    void Start()
    {
        rb_ = GetComponent<Rigidbody>();
        isGrounded = true;
        // Met le curseur au milieu de l'écran
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        rb_.freezeRotation = true;
        // Mouvement Vertical / Horizontal
        float x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float z = Input.GetAxis("Vertical") * speed * Time.deltaTime;



        transform.Translate(x, 0, z);


        // Saut
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {

            rb_.AddForce(new Vector3(0, detente, 0), ForceMode.Impulse);
            isGrounded = false;
            
        }
    }
    void OnCollisionEnter()
    {
        isGrounded = true;
    }
 

}
