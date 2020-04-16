using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Imacube : MonoBehaviour
{
    public string Bonus = "Coin";
    public List<GameObject> BonusChacal;
    public float Offset = 1;
    public Sprite WhenUsed;
    bool CanbeUsed;
    float nbTimes;
    // Start is called before the first frame update
    void Start()
    {
        CanbeUsed = true;
        if (Bonus == "Coin")
        {
            nbTimes = 5;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject objectwhoSpawned = gameObject;
        int index = 0;
        if (CanbeUsed)
        {
            if (collision.gameObject.tag == "Player")
            {
                if (Bonus == "Champi")
                {
                    objectwhoSpawned = Instantiate(BonusChacal[0], new Vector3(transform.position.x, transform.position.y + Offset, transform.position.z), Quaternion.identity);
                    index = 0;
                    //On met le cube en mode je ne peux plus être utilisé
                    SpriteRenderer _spr = gameObject.GetComponent<SpriteRenderer>();
                    _spr.sprite = WhenUsed;
                    CanbeUsed = false;
                }
                if (Bonus == "Coin")
                {
                    objectwhoSpawned = Instantiate(BonusChacal[1], new Vector3(transform.position.x, transform.position.y + Offset, transform.position.z), Quaternion.identity);
                    index = 1;
                    nbTimes -= 1;
                    if (nbTimes <= 0)
                    {
                        //On met le cube en mode je ne peux plus être utilisé
                        SpriteRenderer _spr = gameObject.GetComponent<SpriteRenderer>();
                        _spr.sprite = WhenUsed;
                        CanbeUsed = false;
                    }
                    
                }
                if (Bonus == "FireChampi")
                {
                    objectwhoSpawned = Instantiate(BonusChacal[2], new Vector3(transform.position.x, transform.position.y + Offset, transform.position.z), Quaternion.identity);
                    index = 2;
                    //On met le cube en mode je ne peux plus être utilisé
                    SpriteRenderer _spr = gameObject.GetComponent<SpriteRenderer>();
                    _spr.sprite = WhenUsed;
                    CanbeUsed = false;
                }
                if (objectwhoSpawned.tag == "Collectible")
                {
                    objectwhoSpawned.AddComponent<Collectiblable>();
                    objectwhoSpawned.GetComponent<Collectiblable>().Class = "Bonus." + BonusChacal[index].name;
                }

            }
        }
        
    }
}
