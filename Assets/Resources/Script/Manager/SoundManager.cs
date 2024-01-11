using Path;
using SceneN;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundManager : IManager        // ù BGMȣ�� -> PlayerMoving / CaneraShake / 
{
    AudioSource[] audioSources = new AudioSource[(int)Define.Sound.maxCount];           // BGM  or  Effect  (�Ҹ��߻� �ٿ���) -> MainCamera
    Dictionary<string, AudioClip> _audioClip = new Dictionary<string, AudioClip>();     // ����� Ŭ��

    public void Init()
    {
        string[] soundNames = System.Enum.GetNames(typeof(Define.Sound));

        for (int i = 0; i < soundNames.Length - 1; i++)     // BGM �̳� Effect�� �ϳ��̻� ������ ���� (���� �߻����� �ʵ��� Length �� -1)
        {
            GameObject gObject = new GameObject { name = soundNames[i] };
            audioSources[i] = gObject.AddComponent<AudioSource>();
            audioSources[i].volume = 0.5f;                      // ù ���۽� �������� 0.5f �� �⺻ ����
            gObject.transform.parent = GameManager.SoundP.transform;                    // AudioSource �� �� BGM,Effect �θ� ����  (2��)
        }

        audioSources[(int)Define.Sound.bgm].loop = true;        // BGM �ݺ� ���

        InitVolume();
        // �ʱ� ���� ���� BGM Play
        SceneBGMPlay();
    }

    public void SceneBGMPlay(string scene = "")
    {
        if(scene == "")
            scene = SceneManager.GetActiveScene().name;

        switch (scene)
        {
            case "AdvertisingScene":
                Play(SceneSound_P.LoadingSceneBGM, Define.Sound.bgm);
                break;
            case SceneName.LoginScene:
                Play(SceneSound_P.LoadingSceneBGM, Define.Sound.bgm);
                break;
            case SceneName.LobbyScene:
                Play(SceneSound_P.StartSceneBGM, Define.Sound.bgm);
                break;
            case SceneName.GameScene:
                Play(SceneSound_P.GameSceneBGM, Define.Sound.bgm);
                break;
            default:
                break;
        }
    }

    public void Clear()         // �� ����� ���
    {
        foreach(AudioSource audioSource in audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
        _audioClip.Clear();
    }



    public void Play(AudioClip audioClip, Define.Sound type = Define.Sound.effect, float volum = 1.0f)
    {
        if (audioClip == null)
            return;

        if(type == Define.Sound.bgm)
        {
            AudioSource audioSource = audioSources[(int)Define.Sound.bgm];

            // ���� BGM�ϰ�� �״�� ����
            if (audioClip == audioSource.clip)
                return;

            if (audioSource.isPlaying)                              // ������� BGM�� ������ ����
                audioSource.Stop();

            audioSource.clip = audioClip;
            audioSource.volume = _volume_BGM;
            audioSource.Play();
        }

        if(type == Define.Sound.effect)
        {
            AudioSource audioSource = audioSources[(int)Define.Sound.effect];
            audioSource.volume = _volume_SFX;
            audioSource.PlayOneShot(audioClip);                     // Effect ���� 1���� ����
        }
    }

    public void Play(string path, Define.Sound type = Define.Sound.effect)
    {
        AudioClip audioClip = DefinePathAudioClip(path, type);

        if(type == Define.Sound.effect)
            Play(audioClip, type, _volume_SFX);
        if(type == Define.Sound.bgm)
            Play(audioClip, type, _volume_BGM);

    }

    private AudioClip DefinePathAudioClip(string path, Define.Sound type = Define.Sound.effect)
    {
        AudioClip audioClip = null;

        if(type == Define.Sound.bgm)
        {
            audioClip = Resources.Load<AudioClip>(path);
        }
        if(type == Define.Sound.effect)
        {
            if(_audioClip.TryGetValue(path,out audioClip) == false)     // _audioClip ��ųʸ��� path(Ű��)�� �������� �ʴ´ٸ� ����  (Ǯ���Ұ��̱� ������ �ѹ��� �ִ´�)
            {
                audioClip = Resources.Load<AudioClip>(path);
                _audioClip.Add(path, audioClip);
            }
        }

        return audioClip;
    }

    public void OnUpdate()
    {
        
    }

    #region Volume Mixer
    AudioMixer mixer;

    float _volume_BGM;
    float _volume_SFX;
    float _volume_Mute;

    /// <summary>
    /// ���� ���� �ʱⰪ ����
    /// </summary>
    public void InitVolume()
    {
        if (mixer == null)
            mixer = Resources.Load<AudioMixer>("Art/Sound/Mixer");

        var vol = PlayerPrefs.GetFloat("_volume_Master",0);
        _volume_Mute = vol <= 0 ? 0 : 1;
        SetMute(_volume_Mute);

        vol = PlayerPrefs.GetFloat("_volume_BGM",0.5f);
        SetBGMVal(vol);

        vol = PlayerPrefs.GetFloat("_volume_SFX",0.5f);
        SetSFXVal(vol);
    }

    public void SetMute(float check)
    {
        float vol = check <= 0 ? 0 : 1;
        mixer.SetFloat("Master", check);
        _volume_Mute = vol;
        PlayerPrefs.SetFloat("_volume_Master", vol);
    }
    public void SetSFXVal(float sliderVal)
    {
        mixer.SetFloat("SFX", sliderVal);
        _volume_SFX = sliderVal;
        PlayerPrefs.SetFloat("_volume_SFX", sliderVal);
    }
    public void SetBGMVal(float sliderVal)
    {
        mixer.SetFloat("BGM", sliderVal);
        _volume_BGM = sliderVal;
        PlayerPrefs.SetFloat("_volume_BGM", sliderVal);
    }

    public bool GetMuteF()
    {
        return (_volume_Mute <= 0) ? false : true;
    }
    public float GetBGMF()
    {
        return _volume_BGM;
    }
    public float GetSFXF()
    {
        return _volume_SFX;
    }
    #endregion
}
