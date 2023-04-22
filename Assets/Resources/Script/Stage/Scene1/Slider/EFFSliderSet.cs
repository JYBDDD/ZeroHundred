using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

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
        this.UpdateAsObservable().Where(_ => originVal != currentVal).Subscribe(_ => EffSliderSetting());
    }

    private void Update()
    {
        originVal = slider.value;
    }

    private void EffSliderSetting()
    {
        currentVal = originVal;
        SoundManager.EffSliderPitch = slider.value;
        effAudioSource.volume = SoundManager.EffSliderPitch;
    }
}
