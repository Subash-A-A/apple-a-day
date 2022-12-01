using UnityEngine;

public class AxeHitBox : MonoBehaviour
{
    [SerializeField] float hitDamage = 100f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.GetComponent<Health>().TakeDamage(hitDamage);
        }
    }
}
