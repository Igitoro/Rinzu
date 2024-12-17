using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class NewBehaviourScript : MonoBehaviour
{
    private const float DisabledVolume = -80;
    public Slider slider;
    public AudioMixer audioMixer;
    public string mixerParameter;
    public float minimumVolume;

    void Start()
    {
        slider.SetValueWithoutNotify(GetMixerVolume());
    }

    public void UpdateMixerVolume(float volumeValue)
    {
        SetMixerVolume(volumeValue);
    }

    public void SetMixerVolume(float volumeValue)
    {
        float mixerVolume;
        if (volumeValue == 0)
        {
            mixerVolume = DisabledVolume;
        }
        else
        {
            mixerVolume = Mathf.Lerp(minimumVolume, 0, volumeValue);
        }
        audioMixer.SetFloat(mixerParameter, mixerVolume);
    }

    public float GetMixerVolume()
    {
        audioMixer.GetFloat(mixerParameter, out float mixerVolume);
        if (mixerVolume == DisabledVolume)
        {
            return 0;
        }
        else
        {
            return Mathf.Lerp(1, 0, mixerVolume / minimumVolume);
        }
    }
}
