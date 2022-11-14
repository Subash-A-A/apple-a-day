using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static void StartSlowMotion()
    {
        Time.timeScale = 0.5f;
        Time.fixedDeltaTime = Time.timeScale * .02f;
    }

    public static void StopSlowMotion()
    {
        Time.timeScale = 1f;
    }
}
