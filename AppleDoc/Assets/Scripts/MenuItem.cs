using UnityEngine;
using UnityEngine.UI;

public class MenuItem : MonoBehaviour
{
    [SerializeField] Color baseColor;
    [SerializeField] Color hoverColor;
    [SerializeField] Image pieImg;

    private void Start()
    {
        pieImg.color = baseColor;
    }

    public void Select()
    {
        pieImg.color = hoverColor;
        transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
    }

    public void DeSelect()
    {
        pieImg.color = baseColor;
        transform.localScale = Vector3.one;
    }
}
