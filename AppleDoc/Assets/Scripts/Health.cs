using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] float maxHealth = 100f;
    [SerializeField] Color healthColor;
    [SerializeField] Image healthBarFill;

    private float currentHealth;
    private float healthNormalized;

    private void Start()
    {
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
        currentHealth -= value;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        healthNormalized = currentHealth / maxHealth;

        if (currentHealth < 0.01f)
        {
            Destroy(gameObject);
        }
    }

    private void HealthFillLerper()
    {
        healthBarFill.fillAmount = Mathf.Lerp(healthBarFill.fillAmount, healthNormalized, 10 * Time.unscaledDeltaTime);
    }
}
