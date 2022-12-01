using UnityEngine;

public class ShockWave : MonoBehaviour
{
    [SerializeField] float waveSpeed = 10f;
    private Rigidbody2D rb;
    private void Start()
    {   
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 10f);
    }
    private void FixedUpdate()
    {
        rb.velocity = transform.right * waveSpeed;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Health>().TakeDamage(30f);
        }
    }
}
