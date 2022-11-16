using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Transform attackPoint;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletSpeed = 10f;
    [SerializeField] float bulletDamage = 100f;
    [SerializeField] float delayBetweenShots = 0.5f;
    [SerializeField] float range = 200f;
    [SerializeField] LayerMask target;
    [SerializeField] bool isFullAuto = false;
    
    private LineRenderer line;
    private Transform weaponHolder;
    private Vector2 hitPoint;

    private bool canShoot = true;
    private bool isShooting;

    private void Start()
    {
        canShoot = true;
        weaponHolder = transform.parent;
        line = GetComponent<LineRenderer>();
    }

    private void OnEnable()
    {
        canShoot = true;
    }

    private void Update()
    {
        MyInput();
        GetHitPoint();

        if (isShooting && canShoot)
        {
            StartCoroutine(Shoot());
        }

        DrawLine();
    }

    private void GetHitPoint()
    {
        Ray2D ray = new Ray2D(attackPoint.position, weaponHolder.right);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, range, target);
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
    }
}
