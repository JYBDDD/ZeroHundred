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
        bgmAudioSource = GameManager.SoundP.transform.GetChild(0).GetComponent<AudioSource>();
        this.UpdateAsObservable().Where(_=> originVal != currentVal).Subscribe(_ => EffSliderSetting());
    }

    private void OnEnable()
    {
        slider.value = GameManager.Sound.GetBGMF();
    }

    private void Update()
    {
        originVal = slider.value;
    }

    private void EffSliderSetting()
    {
        currentVal = originVal;
        GameManager.Sound.SetBGMVal(slider.value);
        bgmAudioSource.volume = slider.value;
    }
}
