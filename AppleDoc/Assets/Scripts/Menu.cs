using UnityEngine;

public class Menu : MonoBehaviour
{
    public int selection;
    private int prevSelection;
    private Vector2 mousePosition;
    private float currentAngle;

    public GameObject[] menuItemsArr;

    private MenuItem menuItem;
    private MenuItem prevMenuItem;

    private void Update()
    {
        mousePosition = new Vector2(Input.mousePosition.x - Screen.width / 2, Input.mousePosition.y - Screen.height / 2);
        currentAngle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;

        currentAngle = (currentAngle + 360) % 360;
        selection = (int) (currentAngle / 90);

        if(selection != prevSelection)
        {
            prevMenuItem = menuItemsArr[prevSelection].GetComponent<MenuItem>();
            prevMenuItem.DeSelect();
            prevSelection = selection;

            menuItem = menuItemsArr[selection].GetComponent<MenuItem>();
            menuItem.Select();
        }
    }
}
