using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField] Transform target;
    private void Update()
    {
        transform.position = target.position;
    }
}
