using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missle : MonoBehaviour
{
    public bool firedByPlayer=true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.GetComponent<Collider>().gameObject.tag == "Asteroid")
        {
            Destroy(collision.GetComponent<Collider>().gameObject);
            Destroy(gameObject);
        }
        if (collision.GetComponent<Collider>().gameObject.tag == "Enemy" && firedByPlayer)
        {
            collision.GetComponent<Collider>().gameObject.GetComponent<EnemyController>().loseHealth(1);
            Destroy(gameObject);
        }
        if (collision.GetComponent<Collider>().gameObject.tag == "Player" && !firedByPlayer)
        {
            collision.GetComponent<Collider>().gameObject.GetComponent<PlayerController>().takeDamage(1);
            Destroy(gameObject);
        }
    }
}
