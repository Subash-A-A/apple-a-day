using UnityEngine;
using UnityEngine.UI;

public class KillCounter : MonoBehaviour
{
    [SerializeField] Image fillSlider;
    [SerializeField] ParticleSystem katanaParticle;
    [SerializeField] int maxKillCountForUlt = 10;
    [SerializeField] Color initColor;
    [SerializeField] Color finalColor;
    public int killCount = 0;
    public bool canUlt = false;
    private void Start()
    {
        killCount = 0;
        fillSlider.fillAmount = 0;
        
    }
    private void Update()
    {
        killCount = Mathf.Clamp(killCount, 0, maxKillCountForUlt);
        fillSlider.fillAmount = Mathf.Lerp(fillSlider.fillAmount, (float) killCount/maxKillCountForUlt, 15 * Time.deltaTime);
        canUlt = killCount == maxKillCountForUlt;
        ParticleSystem.MainModule pMain = katanaParticle.main;
        pMain.startColor = Color.Lerp(katanaParticle.main.startColor.color, (canUlt)?finalColor:initColor, 10 * Time.deltaTime);
    }
}
