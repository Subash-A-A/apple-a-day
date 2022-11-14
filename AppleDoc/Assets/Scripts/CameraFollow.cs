using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float followSmooth;
    [SerializeField] float distance = -10f;

    private void Update()
    {   
        Vector3 targetPos = new Vector3(target.position.x, target.position.y, distance);
        transform.position = Vector3.Lerp(transform.position, targetPos, followSmooth * Time.unscaledDeltaTime);
    }
}
