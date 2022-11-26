using UnityEngine;

public class GManager : MonoBehaviour
{
    [SerializeField] private Transform player;
    public Transform GetPlayerTransform()
    {
        return player;
    }
}
