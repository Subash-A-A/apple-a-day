using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 10f;
    public float bulletDamage = 100f;
    [SerializeField] GameObject bulletParticle;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 10f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject particle = Instantiate(bulletParticle, transform.position, Quaternion.identity);
        Destroy(particle, 0.75f);

        if (collision.transform.CompareTag("Enemy"))
        {
            Debug.Log("Enemy Hit!");
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
}
