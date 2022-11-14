using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField] Menu weaponMenu;
    public int currentSelection;

    private void Update()
    {
        currentSelection = weaponMenu.selection;
    }
}
