using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float slowDownRate = 0.125f;
    public void StartSlowMotion()
    {
        Time.timeScale = slowDownRate;
        Time.fixedDeltaTime = Time.timeScale * .02f;
    }

    public void StopSlowMotion()
    {
        Time.timeScale = 1f;
    }
}
