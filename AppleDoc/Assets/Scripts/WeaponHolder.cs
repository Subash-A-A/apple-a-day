using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField] Menu weaponMenu;
    public int currentSelection;

    private void Update()
    {
        currentSelection = weaponMenu.selection;

        if (weaponMenu.gameObject.activeSelf)
        {
            SwapWeapon();
        }
    }

    private void SwapWeapon()
    {
        for(int i = 0; i < transform.childCount - 1; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        transform.GetChild(currentSelection).gameObject.SetActive(true);
    }
}
