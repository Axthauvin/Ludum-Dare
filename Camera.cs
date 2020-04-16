using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    GameObject Player;
    public float MaxHauteurdeMario = 2f;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        float x = Player.transform.position.x;
        
        if (x > transform.position.x)
        {
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }
        float y = Player.transform.position.y;
        if (y > MaxHauteurdeMario)
        {
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, 2, transform.position.z);
        }

    }
}
