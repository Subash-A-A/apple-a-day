using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Transform attackPoint;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float range = 200f;
    [SerializeField] LayerMask target;
    
    private LineRenderer line;
    private Transform weaponHolder;
    private Vector2 hitPoint;

    private void Start()
    {   
        weaponHolder = transform.parent;
        line = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        GetHitPoint();

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
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
            hitPoint = ray.GetPoint(range);
        }
    }

    private void Shoot()
    {
        Instantiate(bulletPrefab, attackPoint.position, weaponHolder.rotation);
    }

    private void DrawLine()
    {
        line.SetPosition(0, attackPoint.position);
        line.SetPosition(1, hitPoint);
    }
}
