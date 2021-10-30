using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public Vector3 rotation = Vector3.zero;
    public float moveSpeed = 1000f;
	public float thrustSpeed = 10f;
    private Vector3 inputRotation;
    private Vector3 mousePlacement;
    private Vector3 screenCentre;
    private Rigidbody rb;
    public GameObject misslePrefab;

    private float missleThrustSpeed = 3000f;
    public float fireSpeed=1f;
    public bool canShoot = true;
    public Transform[] bulletSpawns;
    public int bulletSpawnNext = 0;
    public int playerHealth = 3;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

	void FixedUpdate()
	{

        rb.velocity = new Vector3(Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed,
            Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed, 0);


        if (Input.GetButton("Fire1"))
        {
            if (canShoot)
            {
                GameObject missle = Instantiate(misslePrefab, (Vector3)bulletSpawns[bulletSpawnNext].position, transform.rotation);
                bulletSpawnNext = (bulletSpawnNext < bulletSpawns.Length-1) ? bulletSpawnNext+1 : 0;
                missle.transform.rotation = Quaternion.LookRotation(Vector3.up);
                missle.GetComponent<Rigidbody>().AddRelativeForce(Vector3.left * missleThrustSpeed);
                canShoot = false;

                StartCoroutine(ShootCoroutine());
            }
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

    void OnTriggerEnter(Collider collision)
    {
        if (collision.GetComponent<Collider>().gameObject.tag == "Asteroid")
        {
            Destroy(collision.GetComponent<Collider>().gameObject);
            takeDamage(1);
        }
    }
    public void takeDamage(int damage)
    {
        playerHealth -= damage;
        if (playerHealth <= 0)
        {
            playerDie();
        }

    }
    void playerDie()
    {
        Destroy(gameObject);
    }

}
