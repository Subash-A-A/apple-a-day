using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class EffectManager : MonoBehaviour
{
    private Volume volume;

    private void Start()
    {
        volume = GetComponent<Volume>();
    }

    public void ChangeChromaticAbb(float value)
    {
        if (volume.profile.TryGet<ChromaticAberration>(out ChromaticAberration aberration))
        {
            aberration.intensity.value = Mathf.Lerp(aberration.intensity.value, value, 5 * Time.unscaledDeltaTime);
        }
    }

    public void ChangeSaturation(float value)
    {
        if(volume.profile.TryGet<ColorAdjustments>(out ColorAdjustments color))
        {
            color.saturation.value = Mathf.Lerp(color.saturation.value, value, 5 * Time.unscaledDeltaTime);
        }
    }

    public void SetDOF(bool value)
    {
        if (volume.profile.TryGet<DepthOfField>(out DepthOfField dof))
        {
            dof.active = value;
        }
    }
}
