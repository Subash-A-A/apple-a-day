using UnityEngine;
using UnityEngine.UI;

public class MenuItem : MonoBehaviour
{
    [SerializeField] Color baseColor;
    [SerializeField] Color hoverColor;
    [SerializeField] Image pieImg;

    private Vector3 localScale;

    private void Start()
    {
        pieImg.color = baseColor;
        localScale = Vector3.one;
    }

    private void Update()
    {
        ScaleLerper();
    }

    public void Select()
    {
        pieImg.color = hoverColor;
        localScale = new Vector3(1.1f, 1.1f, 1.1f);
    }

    public void DeSelect()
    {
        pieImg.color = baseColor;
        localScale = Vector3.one;
    }

    private void ScaleLerper()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, localScale, 10 * Time.unscaledDeltaTime);
    }
}
