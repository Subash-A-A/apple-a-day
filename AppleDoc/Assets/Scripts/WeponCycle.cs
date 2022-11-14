using UnityEngine;

public class WeponCycle : MonoBehaviour
{
    [SerializeField] KeyCode CycleKey;
    [SerializeField] TimeManager timeManager;
    [SerializeField] EffectManager effectManager;
    [SerializeField] GameObject menu;
    private MouseAim aim;

    private void Start()
    {
        aim = GetComponent<MouseAim>();
        menu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKey(CycleKey))
        {
            menu.SetActive(true);
            aim.canAim = false;
            effectManager.ChangeChromaticAbb(1f);
            effectManager.ChangeSaturation(-100f);
            effectManager.SetDOF(true);
            timeManager.StartSlowMotion();
        }
        else
        {
            menu.SetActive(false);
            aim.canAim = true;
            effectManager.ChangeChromaticAbb(0f);
            effectManager.ChangeSaturation(0f);
            effectManager.SetDOF(false);
            timeManager.StopSlowMotion();
        }
    }
}
