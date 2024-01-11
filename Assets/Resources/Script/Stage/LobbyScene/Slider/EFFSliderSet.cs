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
        effAudioSource = GameManager.SoundP.transform.GetChild(1).GetComponent<AudioSource>();
        this.UpdateAsObservable().Where(_ => originVal != currentVal).Subscribe(_ => EffSliderSetting());
    }

    private void OnEnable()
    {
        slider.value = GameManager.Sound.GetSFXF();
    }

    private void Update()
    {
        originVal = slider.value;
    }

    private void EffSliderSetting()
    {
        currentVal = originVal;
        GameManager.Sound.SetSFXVal(slider.value);
        effAudioSource.volume = slider.value;
    }
}
