using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

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

        this.UpdateAsObservable().Where(_=> originVal != currentVal).Subscribe(_ => EffSliderSetting());
    }

    private void Update()
    {
        originVal = slider.value;
    }

    private void EffSliderSetting()
    {
        currentVal = originVal;
        SoundManager.BGMSliderPitch = slider.value;
        bgmAudioSource.volume = SoundManager.BGMSliderPitch;
    }
}
