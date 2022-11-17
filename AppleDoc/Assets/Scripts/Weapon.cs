using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    public bool isMeeleWeapon;
    [SerializeField] float meeleDashPower = 10f;
    [SerializeField] float meeleDashTime = 0.2f;
    [SerializeField] float meeleDashCooldown = 1f;
    [SerializeField] TrailRenderer tr;
    [SerializeField] GameObject meeleHitBox;

    [SerializeField] GameObject player;
    [SerializeField] Transform attackPoint;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float impactForce = 10f;
    [SerializeField] float bulletSpeed = 10f;
    [SerializeField] float bulletDamage = 100f;
    [SerializeField] float delayBetweenShots = 0.5f;
    [SerializeField] float range = 200f;
    [SerializeField] LayerMask targetLayer;

    [SerializeField] bool isFullAuto = false;
    [SerializeField] bool useTorch = true;
    [SerializeField] GameObject torchLight;
    
    private LineRenderer line;
    private PlayerMovement playerMovement;
    private Rigidbody2D playerRb;
    private Collider2D playerColl;
    private Transform weaponHolder;
    private Vector2 hitPoint;

    private bool canShoot = true;
    private bool isShooting;

    private void Start()
    {
        canShoot = true;
        weaponHolder = transform.parent;
        line = GetComponent<LineRenderer>();
        playerMovement = player.GetComponent<PlayerMovement>();
        playerRb = player.GetComponent<Rigidbody2D>();
        playerColl = player.GetComponent<Collider2D>();

        torchLight.SetActive(useTorch);
    }

    private void OnEnable()
    {
        canShoot = true;
        torchLight.SetActive(useTorch);

        if (isMeeleWeapon)
        {
            meeleHitBox.SetActive(false);
            playerMovement.canDash = true;
            playerMovement.isDashing = false;
            tr.emitting = false;
            playerRb.velocity = Vector2.right * 0f;
            playerColl.enabled = true;
            playerRb.gravityScale = 1f;
        }
    }

    private void Update()
    {
        MyInput();
        
        if (isMeeleWeapon)
        {
            if (isShooting)
            {
                MeeleDash();
            }
        }

        if(!isMeeleWeapon)
        {
            GetHitPoint();
            DrawLine();
        }

        if (isShooting && canShoot && !isMeeleWeapon)
        {   
            StartCoroutine(Shoot());
        }
    }

    private void GetHitPoint()
    {
        Ray2D ray = new Ray2D(attackPoint.position, weaponHolder.right);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, range, targetLayer);
        if (hit.collider != null)
        {
            hitPoint = hit.point;
        }
        else
        {
            hitPoint = ray.GetPoint(100f);
        }
    }

    private IEnumerator Shoot()
    {
        SpawnBullet();
        canShoot = false;
        yield return new WaitForSeconds(delayBetweenShots);
        canShoot = true;
    }

    private void DrawLine()
    {
        line.SetPosition(0, attackPoint.position);
        line.SetPosition(1, hitPoint);
    }

    private void MyInput()
    {
        isShooting = isFullAuto ? Input.GetButton("Fire1") : Input.GetButtonDown("Fire1");
    }

    private void SpawnBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, attackPoint.position, weaponHolder.rotation);
        Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), player.GetComponent<Collider2D>());
        Bullet bulletScr = bullet.GetComponent<Bullet>();
        bulletScr.bulletSpeed = bulletSpeed;
        bulletScr.bulletDamage = bulletDamage;
        bulletScr.travelDistance = range;
        bulletScr.spawnPoint = attackPoint.position;
        bulletScr.impactForce = impactForce;
    }

    private void MeeleDash()
    {
        if (playerMovement.canDash)
        {
            StartCoroutine(playerMovement.PlayerDashAttack(player.transform.right, meeleDashPower, meeleDashTime, meeleDashCooldown, tr, meeleHitBox));
        }
    }
}
