using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField] Menu weaponMenu;
    [SerializeField] Transform meelePoint;
    [SerializeField] Transform weaponPoint;
    public int currentSelection;

    private bool isUsingMeele;

    private void Start()
    {
        DisableAllWeapons();
        transform.GetChild(currentSelection).gameObject.SetActive(true);
    }

    private void Update()
    {
        currentSelection = weaponMenu.selection;

        if (weaponMenu.gameObject.activeSelf)
        {
            SwapWeapon();
        }

        MeeleEquipCheck();
        transform.position = (isUsingMeele) ? meelePoint.position : weaponPoint.position;
    }

    private void SwapWeapon()
    {
        DisableAllWeapons();
        transform.GetChild(currentSelection).gameObject.SetActive(true);
    }

    private void MeeleEquipCheck()
    {
        isUsingMeele = transform.GetChild(currentSelection).CompareTag("MeeleWeapon");
    }

    private void DisableAllWeapons()
    {
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
