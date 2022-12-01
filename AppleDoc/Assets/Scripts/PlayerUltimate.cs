using UnityEngine;

public class PlayerUltimate : MonoBehaviour
{   
    [SerializeField] AudioManager audioManager;

    private Gradient originalColor;
    private Rigidbody2D rb;
    public static bool canKatanaSlash;
    private Animator camAnim;
    private PlayerMovement playerMovement;
    // private CameraFollow camFollow;

    [SerializeField] TrailRenderer tr;
    [SerializeField] Gradient bankaiColor;
    [SerializeField] ParticleSystem katanaParticle;

    private void Start()
    {
        // camFollow = FindObjectOfType<CameraFollow>();
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
        camAnim = Camera.main.GetComponent<Animator>();
        originalColor = tr.colorGradient;
    }
    public void BankaiInit()
    {
        audioManager.Play("KatanaCharge");
        camAnim.SetBool("loopShake", true);
        tr.colorGradient = bankaiColor;
        tr.emitting = true;
        rb.velocity = new Vector2(rb.velocity.x * 0.1f, rb.velocity.y * 0.2f);
        rb.gravityScale = 0f;
        playerMovement.enabled = false;
        katanaParticle.Play();
    }

    public void BankaiSlash()
    {
        audioManager.Play("KatanaShockWave");
        canKatanaSlash = true;
        FindObjectOfType<KillCounter>().killCount = 0;
    }

    public void BankaiEnd()
    {
        camAnim.SetBool("loopShake", false);
        canKatanaSlash = false;
        tr.colorGradient = originalColor;
        tr.emitting = false;
        rb.gravityScale = 1f;
        playerMovement.enabled = true;
        katanaParticle.Stop();
    }
}
