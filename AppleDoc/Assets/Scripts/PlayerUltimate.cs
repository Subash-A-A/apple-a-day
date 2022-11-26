using UnityEngine;

public class PlayerUltimate : MonoBehaviour
{   
    [SerializeField] AudioManager audioManager;

    private Gradient originalColor;
    private Rigidbody2D rb;
    public static bool canKatanaSlash;
    private Animator camAnim;
    // private CameraFollow camFollow;

    [SerializeField] TrailRenderer tr;
    [SerializeField] Gradient bankaiColor;
    [SerializeField] ParticleSystem katanaParticle;

    private void Start()
    {
        // camFollow = FindObjectOfType<CameraFollow>();
        rb = GetComponent<Rigidbody2D>();
        camAnim = Camera.main.GetComponent<Animator>();
        originalColor = tr.colorGradient;
    }
    public void BankaiInit()
    {
        // camFollow.ChangeDistance(-5f);
        audioManager.Play("KatanaCharge");
        camAnim.SetBool("loopShake", true);
        tr.colorGradient = bankaiColor;
        tr.emitting = true;
        rb.simulated = false;
        katanaParticle.Play();
    }

    public void BankaiSlash()
    {
        // camFollow.ChangeDistance(-10f);
        audioManager.Play("KatanaShockWave");
        rb.simulated = true;
        canKatanaSlash = true;
    }

    public void BankaiEnd()
    {
        camAnim.SetBool("loopShake", false);
        canKatanaSlash = false;
        tr.colorGradient = originalColor;
        tr.emitting = false;
        katanaParticle.Stop();
    }
}
