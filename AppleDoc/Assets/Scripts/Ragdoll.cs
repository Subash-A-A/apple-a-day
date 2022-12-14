using UnityEngine;
using UnityEngine.Rendering.Universal;
public class Ragdoll : MonoBehaviour
{
    [SerializeField] Collider2D[] colliderArr;
    [SerializeField] GameObject uiCanvas;

    private Animator anim;
    private Collider2D mainCollider;
    private Rigidbody2D mainRigidbody;
    private ShadowCaster2D shadowCaster;

    private void Start()
    {   
        anim = GetComponent<Animator>();
        mainCollider = GetComponent<Collider2D>();
        mainRigidbody = GetComponent<Rigidbody2D>();
        shadowCaster = GetComponent<ShadowCaster2D>();

        foreach (var collider in colliderArr)
        {
            collider.enabled = false;
        }
    }
    public void ActivateRagdoll()
    {   
        if(TryGetComponent<EnemyBrain>(out var brain))
        {
            brain.enabled = false;
        }

        if (TryGetComponent<PlayerMovement>(out var player))
        {
            GetComponent<WeponCycle>().enabled = false;
            GameObject weaponHolder = GetComponentInChildren<WeaponHolder>().gameObject;
            Destroy(weaponHolder);
            player.enabled = false;
        }

        if (TryGetComponent<BossAttacks>(out var boss))
        {
            boss.enabled = false;
        }

        Destroy(mainRigidbody);
        mainCollider.enabled = false;
        anim.enabled = false;
        if (shadowCaster)
        {
            shadowCaster.enabled = false;
        }
        uiCanvas.SetActive(false);

        foreach (var collider in colliderArr)
        {
            Rigidbody2D rb = collider.gameObject.AddComponent<Rigidbody2D>();
            rb.AddForce(transform.up * 2f, ForceMode2D.Impulse);
            rb.interpolation = RigidbodyInterpolation2D.Interpolate;
            rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            collider.enabled = true;
        }

        Destroy(gameObject, 10f);
    }
}
