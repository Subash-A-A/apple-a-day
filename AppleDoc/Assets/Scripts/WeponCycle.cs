using UnityEngine;

public class WeponCycle : MonoBehaviour
{
    [SerializeField] KeyCode CycleKey;
    [SerializeField] TimeManager timeManager;
    [SerializeField] EffectManager effectManager;
    [SerializeField] GameObject menu;

    private void Start()
    {
        menu.SetActive(false);
    }
    
    private void Update()
    {
        if (Input.GetKey(CycleKey) || Input.GetButton("Fire3"))
        {
            menu.SetActive(true);
            effectManager.ChangeChromaticAbb(1f);
            effectManager.ChangeSaturation(-100f);
            effectManager.SetDOF(true);
            timeManager.StartSlowMotion();
        }
        else
        {
            menu.SetActive(false);
            effectManager.ChangeChromaticAbb(0f);
            effectManager.ChangeSaturation(0f);
            effectManager.SetDOF(false);
            timeManager.StopSlowMotion();
        }
    }
}
