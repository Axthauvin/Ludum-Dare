using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coinesque : MonoBehaviour
{
    Vector2 originalPos;
    public float upper = 2;
    public float speed = 0.1f;
    Vector2 togopos;
    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.position;
        togopos.x = originalPos.x;
        togopos.y = originalPos.y + upper;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
        if (transform.position.y >= togopos.y)
        {
            Destroy(gameObject);
        }
    }
}
