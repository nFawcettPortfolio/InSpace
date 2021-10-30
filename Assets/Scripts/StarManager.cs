using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarManager : MonoBehaviour
{
    public GameObject star;
    public int starCount = 50;
    public float starMove = 1f;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < starCount; i++)
        {
            generateStars(i);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    void generateStars(int i)
    {
        GameObject s = Instantiate(star, new Vector3(i * Random.Range(2f, 10f), i * Random.Range(2f, 10f), i * Random.Range(2f, 10f)),
            Quaternion.Euler(new Vector3(Random.Range(-90, 90), Random.Range(-90, 90), Random.Range(30, 60))));
        s.tag = "Star";
        s.GetComponent<Rigidbody>().velocity = Vector3.down * starMove;
    }

}
