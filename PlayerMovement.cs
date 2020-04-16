using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerController controller;
    public float runSpeed = 40f;
    public Animator animator;

    float horizontalmove = 0f;
    bool jump = false;
    bool crouch;
    Rigidbody2D rb_;

    float normal_size;
    float little_size;
    bool little;

    public GameObject FireBall;
    bool IsEnlair;
    bool Nomove;
    float NormalGravity;
    public float AfterSalto = 2;
    bool salto;
    bool Iscrrouch;
    bool encours;
    // Start is called before the first frame update
    void Start()
    {
        normal_size = transform.localScale.x;
        little_size = normal_size / 1.5f;
        rb_ = gameObject.GetComponent<Rigidbody2D>();
        little = true;
        Nomove = false;
        NormalGravity = rb_.gravityScale;
        salto = false;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Vertical") < 0)
        {
            Iscrrouch = true;
        }
        else
        {
            Iscrrouch = false;
        }
        animator.SetBool("Grounded", !IsEnlair);
        if (Nomove)
        {
            if (gameObject.transform.localRotation.z > -0.1 && gameObject.transform.localRotation.z < 0)
            {
                salto = false;
                animator.SetBool("Salto", salto);
                Nomove = false;
                ChangeGravityAugmente();
            }
        }
        NomoveActual(Nomove);
        if (little)
        {
            gameObject.transform.localScale = new Vector3(little_size, little_size, little_size);
        }
        else
        {
            gameObject.transform.localScale = new Vector3(normal_size, normal_size, normal_size);
        }
        
        if (!salto)
        {
            animator.SetFloat("Speed", Mathf.Abs(horizontalmove));
        }
        else
        {
            animator.SetFloat("Speed", 0);
        }
        horizontalmove = Input.GetAxisRaw("Horizontal") * runSpeed;
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            IsEnlair = true;
            animator.SetBool("Jump", true);
        }
        if (Input.GetButtonDown("Crouch") || Iscrrouch)
        {
            if (gameObject.GetComponent<PlayerController>().m_Grounded)
            {
                crouch = true;
            }
            

        } else if (Input.GetButtonUp("Crouch") || !Iscrrouch)
        {
            crouch = false;
        }
        if ((Input.GetButtonDown("Jump") && (Input.GetButton("Crouch"))) || (Input.GetButtonDown("Jump") && Iscrrouch)) //Super Saut
        {
            rb_.AddForce(new Vector2(0, 50));
        }
        if (((IsEnlair && (Input.GetButtonDown("Crouch"))) || (IsEnlair && Iscrrouch)) && !Input.GetButton("Jump")) //Salto
        {
            if (!encours)
            {
                salto = true;
                animator.SetBool("Salto", salto);
                Debug.Log("Et ouais mon pote");
                Nomove = true;
                encours = true;
            }
            
            
            
        }
        if (Input.GetButton("Fire1"))
        {
            animator.SetBool("Shout", true);

        } else if (Input.GetButtonDown("Fire1"))
        {
            animator.SetBool("Shout", true);
        }
        else
        {
            animator.SetBool("Shout", false);
        }

    }
    public void OnLanding()
    {
        animator.SetBool("Jump", false);
    }
    public void OnCrouching(bool Iscrouch)
    {
        animator.SetBool("Crouch", Iscrouch);
    }
    private void FixedUpdate()
    {
        controller.Move(horizontalmove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }
    public void NewEffect(string Effect)
    {
        if(Effect == "Bonus.Champi")
        {
            little = false;
        }
        if (Effect == "Bonus.FireChampi")
        {
            Debug.Log("I'm on fire");
            animator.SetBool("IsFire", true);
        }
    }
    public void Shout()
    {
        Transform origin = this.gameObject.transform.GetChild(3);
        Instantiate(FireBall, origin.position, origin.rotation);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            IsEnlair = false;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            IsEnlair = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            IsEnlair = true;
        }
    }
    void NomoveActual(bool yes)
    {
        if (yes)
        {
            rb_.constraints = RigidbodyConstraints2D.FreezeAll;
            
        }
        else
        {
            rb_.constraints = RigidbodyConstraints2D.None;
            rb_.constraints = RigidbodyConstraints2D.FreezeRotation;
            
        }
    }
    public void ChangeGravityAugmente()
    {
        rb_.gravityScale = AfterSalto;
    }
    public void ChangeGravityNormal()
    {
        rb_.gravityScale = NormalGravity;
    }

}
