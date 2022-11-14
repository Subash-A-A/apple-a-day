using UnityEngine;

public class WeponCycle : MonoBehaviour
{
    [SerializeField] KeyCode CycleKey;
    [SerializeField] TimeManager timeManager;
    private MouseAim aim;

    private void Start()
    {
        aim = GetComponent<MouseAim>();
    }

    private void Update()
    {
        if (Input.GetKey(CycleKey))
        {
            aim.canAim = false;
            timeManager.StartSlowMotion();
        }
        else
        {
            aim.canAim = true;
            timeManager.StopSlowMotion();
        }
    }
}
