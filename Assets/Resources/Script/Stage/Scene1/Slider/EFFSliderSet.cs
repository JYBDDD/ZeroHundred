using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EFFSliderSet : MonoBehaviour
{
    AudioSource effAudioSource;

    Slider slider;

    float originVal;
    float currentVal;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        slider.value = SoundManager.EffSliderPitch;
        effAudioSource = GameManager.SoundP.transform.GetChild(1).GetComponent<AudioSource>();
    }

    private void Update()
    {
        originVal = slider.value;
        if (originVal != currentVal)
            EffSliderSetting();
    }

    private void EffSliderSetting()
    {
        currentVal = originVal;
        SoundManager.EffSliderPitch = slider.value;
        effAudioSource.volume = SoundManager.EffSliderPitch;
    }
}
