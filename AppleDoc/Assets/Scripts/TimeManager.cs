using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float slowDownRate = 0.1f;
    private float startFixedDeltaTime;
    private float startTimeScale;

    private void Start()
    {
        startTimeScale = Time.timeScale;
        startFixedDeltaTime = Time.fixedDeltaTime;
    }

    public void StartSlowMotion()
    {
        Time.timeScale = slowDownRate;
        Time.fixedDeltaTime = startFixedDeltaTime * slowDownRate;
    }

    public void StopSlowMotion()
    {
        Time.timeScale = startTimeScale;
        Time.fixedDeltaTime = startFixedDeltaTime;
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }
}
