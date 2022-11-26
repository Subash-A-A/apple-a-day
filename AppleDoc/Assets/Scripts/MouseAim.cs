using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseAim : MonoBehaviour
{
    [SerializeField] Transform weaponHolder;
    [SerializeField] Transform uiCanvas;
    [SerializeField] Camera cam;

    public bool canAim;

    private Vector2 mousePosition;
    private bool isFacingRight;

    private void Start()
    {
        isFacingRight = true;
        canAim = true;
    }
    private void Update()
    {
        if (canAim && weaponHolder != null)
        {
            GetMousePosition();
            FlipPlayer();
            AimWeapon();
        }
    }

    private void GetMousePosition()
    {
        Vector2 screenCentre = new Vector2(Screen.width / 2, Screen.height / 2);
        mousePosition = new Vector2(Input.mousePosition.x - screenCentre.x, Input.mousePosition.y - screenCentre.y);
    }

    private void FlipPlayer()
    {
        if (mousePosition.x > transform.position.x)
        {
            transform.localRotation = Quaternion.identity;
            uiCanvas.localRotation = Quaternion.identity;
            isFacingRight = true;
        }
        else if (mousePosition.x < transform.position.x && isFacingRight)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
            uiCanvas.localRotation = Quaternion.Euler(0, 180, 0);
            isFacingRight = false;
        }
    }

    private void AimWeapon()
    {
        float aimAngle = Mathf.Atan2(mousePosition.y, Mathf.Abs(mousePosition.x)) * Mathf.Rad2Deg;
        weaponHolder.localRotation = Quaternion.Euler(0, 0, aimAngle); 
    }
}
