using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    public GameObject[] asteroids;
    public int asteroidCount = 50;
    public float asteroidMove = 5f;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < asteroidCount; i++)
        {
            GameObject a = Instantiate(asteroids[Random.Range(0, asteroids.Length)], new Vector3(i * Random.Range(2f, 5f), (i * Random.Range(2f, 5f))+40, 0),
                Quaternion.Euler(new Vector3(Random.Range(-90, 90), Random.Range(-90, 90), Random.Range(-90, 90))));
            a.tag = "Asteroid";
            a.GetComponent<Rigidbody>().velocity = Vector3.down * asteroidMove;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
