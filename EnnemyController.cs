using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyController : MonoBehaviour
{
    public List<Transform> Waypoints;
    public float Rushspeed;
    public float NormalSpeed;
    public float LavueduMec;
    public bool Gauche;
    public bool JeSuisCon;
    public string Class;
    int i = 0;
    Transform Head;
    float oldx = 0.0f;
    public bool PlayerDetected;
    public bool Gotodeath;
    // Start is called before the first frame update
    void Start()
    {
        i = 0;
        if(Class == "Goomba")
        {
            Head = this.gameObject.transform.GetChild(0);
            Head.gameObject.AddComponent<Heady>();
        }
        oldx = transform.position.x;
        PlayerDetected = false;
        Gotodeath = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Class == "Goomba")
        {
            if (JeSuisCon)
            {
                this.gameObject.transform.Translate(-NormalSpeed * Time.deltaTime, 0, 0);
            }
            else
            {
                if (!PlayerDetected)
                {
                    Vector3 yep = Waypoints[i].transform.position;
                    yep.y = gameObject.transform.position.y;
                    yep.z = gameObject.transform.position.z;
                    gameObject.transform.position = Vector3.MoveTowards(transform.position, yep, NormalSpeed * Time.deltaTime);
                    if (gameObject.transform.position == yep)
                    {
                        i += 1;
                    }
                    if (i == Waypoints.Count)
                    {
                        i = 0;
                    }
                }

                Vector3 origin = transform.position;
                Vector3 direction;
                if (Gauche) // Ici, on  change la direction de notre Raycast !
                {
                    direction = transform.TransformDirection(Vector2.left);

                }
                else
                {
                    direction = transform.TransformDirection(Vector2.right);
                }
                RaycastHit hit;
                Debug.DrawRay(origin, direction * LavueduMec, Color.red);
                if (Physics.Raycast(origin, direction, out hit, LavueduMec))
                {
                    if (hit.collider.gameObject.tag == "Player")
                    {
                        PlayerDetected = true;
                        gameObject.transform.position = Vector3.MoveTowards(transform.position, hit.collider.gameObject.transform.position, Rushspeed * Time.deltaTime);
                    }

                }
                else
                {
                    PlayerDetected = false;
                }

            }
            if (Gotodeath)
            {
                gameObject.transform.Translate(0, -0.1f * Time.deltaTime, 0);
                if (transform.position.y <= -0.05)
                {
                    Destroy(gameObject);
                }
            }
        }
        


    }
    private void FixedUpdate()
    {
        if (!PlayerDetected)
        {
            if (oldx - transform.position.x > 0) // he's looking right
            {
                Gauche = true;
            }

            if (oldx - transform.position.x < 0) // he's looking left
            {
                Gauche = false;
            }
            oldx = transform.position.x;
        }
        
        
    }
}
