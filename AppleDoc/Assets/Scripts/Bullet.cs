using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 10f;
    public float bulletDamage = 100f;
    public float impactForce = 10f;
    public float travelDistance = 100f;
    [SerializeField] GameObject bulletParticle;

    [HideInInspector]
    public Vector2 spawnPoint;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 10f);
    }
    private void Update()
    {
        if (Vector2.Distance(spawnPoint, transform.position) >= travelDistance)
        {
            SpawnParticle();
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SpawnParticle();

        if (collision.transform.CompareTag("Enemy"))
        {
            collision.rigidbody.velocity = new Vector2(0f, collision.rigidbody.velocity.y);
            collision.rigidbody.AddForce(transform.right * impactForce, ForceMode2D.Impulse);
            DealDamage(collision.gameObject);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.right * bulletSpeed;
    }

    private void DealDamage(GameObject obj)
    {   
        obj.GetComponent<Health>().TakeDamage(bulletDamage);
    }

    private void SpawnParticle()
    {
        GameObject particle = Instantiate(bulletParticle, transform.position, Quaternion.identity);
        Destroy(particle, 0.75f);
    }
}
