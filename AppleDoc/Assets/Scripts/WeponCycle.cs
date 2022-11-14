using UnityEngine;

public class WeponCycle : MonoBehaviour
{
    [SerializeField] KeyCode CycleKey;

    private void Update()
    {
        if (Input.GetKey(CycleKey))
        {
            TimeManager.StartSlowMotion();
        }
        else
        {
            TimeManager.StopSlowMotion();
        }
    }
}
