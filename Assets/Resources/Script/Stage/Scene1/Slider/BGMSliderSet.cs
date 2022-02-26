using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGMSliderSet : MonoBehaviour
{
    AudioSource bgmAudioSource;

    Slider slider;

    float originVal;
    float currentVal;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        slider.value = SoundManager.BGMSliderPitch;
        bgmAudioSource = GameManager.SoundP.transform.GetChild(0).GetComponent<AudioSource>();
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
        SoundManager.BGMSliderPitch = slider.value;
        bgmAudioSource.volume = SoundManager.BGMSliderPitch;
    }
}
