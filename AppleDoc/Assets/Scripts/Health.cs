using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    [SerializeField] Color healthColor;
    [SerializeField] Image healthBarFill;
    [SerializeField] float hitImpact = 2f;

    private float currentHealth;
    private float healthNormalized;
    private Animator anim;
    private Rigidbody2D rb;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        healthBarFill.color = healthColor;
        healthBarFill.fillAmount = 1;
        healthNormalized = currentHealth / maxHealth;
    }

    private void Update()
    {
        HealthFillLerper();
    }

    public void TakeDamage(float value)
    {
        anim.SetTrigger("Hit");
        rb.AddForce(hitImpact * rb.mass * (transform.up + transform.right * -1).normalized, ForceMode2D.Impulse);

        currentHealth -= value;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        healthNormalized = currentHealth / maxHealth;

        if (currentHealth < 0.01f)
        {
            if(TryGetComponent<EnemyBrain>(out EnemyBrain brain))
            {
                brain.GetComponent<Ragdoll>().ActivateRagdoll();
            }

            if (TryGetComponent<PlayerMovement>(out PlayerMovement player))
            {
                player.GetComponent<Ragdoll>().ActivateRagdoll();
            }
        }
    }

    private void HealthFillLerper()
    {
        healthBarFill.fillAmount = Mathf.Lerp(healthBarFill.fillAmount, healthNormalized, 10 * Time.unscaledDeltaTime);
    }
}
