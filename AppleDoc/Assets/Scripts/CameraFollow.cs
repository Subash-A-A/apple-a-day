using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float followSmooth;
    [SerializeField] float distance = -10f;
    
    public float followYOffset = 0f;

    private void Update()
    {
        if (target)
        {
            Vector3 targetPos = new(target.position.x, target.position.y + followYOffset, distance);
            transform.position = Vector3.Lerp(transform.position, targetPos, followSmooth * Time.unscaledDeltaTime);
        }
    }

    public void ChangeDistance(float dist)
    {
        distance = dist;
    }
}
