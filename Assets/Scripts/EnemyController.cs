using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameObject player;
    private Transform t;
    private Rigidbody rb;

    public int enemyHealth = 5;
    public float moveSpeed = 1000f;

    public Transform[] bulletSpawns;
    public int bulletSpawnNext = 0;
    public GameObject misslePrefab;
    public float fireSpeed = 1f;
    public bool canShoot = true;
    private float missleThrustSpeed = 3000f;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        t = gameObject.GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<Transform>().position.x > t.position.x)
        {
            rb.velocity = Vector3.right * Time.deltaTime * moveSpeed;
        }
        else if (player.GetComponent<Transform>().position.x < t.position.x)
        {
            rb.velocity = Vector3.left * Time.deltaTime * moveSpeed;
        }
        if (player.GetComponent<Transform>().position.x > t.position.x + 2 ||
            player.GetComponent<Transform>().position.x < t.position.x - 2)
        {
            fireMissle();
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.GetComponent<Collider>().gameObject.tag == "Asteroid")
        {
            Destroy(collision.GetComponent<Collider>().gameObject);
        }
    }

    void fireMissle()
    {
        if (canShoot)
        {
            GameObject missle = Instantiate(misslePrefab, (Vector3)bulletSpawns[bulletSpawnNext].position, transform.rotation);
            bulletSpawnNext = (bulletSpawnNext < bulletSpawns.Length - 1) ? bulletSpawnNext + 1 : 0;
            missle.transform.rotation = Quaternion.LookRotation(Vector3.down);
            missle.GetComponent<Rigidbody>().AddRelativeForce(Vector3.left * missleThrustSpeed);
            missle.GetComponent<Missle>().firedByPlayer = false;
            canShoot = false;

            StartCoroutine(ShootCoroutine());
        }
    }
    public static IEnumerator WaitForRealSeconds(float delay)
    {
        float start = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup < start + delay)
        {
            yield return null;
        }
    }
    private IEnumerator ShootCoroutine()
    {
        yield return StartCoroutine(WaitForRealSeconds(fireSpeed));
        canShoot = true;
    }
    public void loseHealth(int damageRecieved)
    {
        enemyHealth -= damageRecieved;
        if (enemyHealth <= 0){
            Destroy(gameObject);
        }
    }



}
