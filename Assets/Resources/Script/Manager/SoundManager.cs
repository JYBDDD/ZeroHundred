using Path;
using SceneN;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : IManager        // ù BGMȣ�� -> PlayerMoving / CaneraShake / 
{
    public static float BGMSliderPitch = 0.5f;
    public static float EffSliderPitch = 0.5f;

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
            audioSource.volume = BGMSliderPitch;
            audioSource.Play();
        }

        if(type == Define.Sound.effect)
        {
            AudioSource audioSource = audioSources[(int)Define.Sound.effect];
            audioSource.volume = EffSliderPitch;
            audioSource.PlayOneShot(audioClip);                     // Effect ���� 1���� ����
        }
    }

    public void Play(string path, Define.Sound type = Define.Sound.effect)
    {
        AudioClip audioClip = DefinePathAudioClip(path, type);

        if(type == Define.Sound.effect)
            Play(audioClip, type, EffSliderPitch);
        if(type == Define.Sound.bgm)
            Play(audioClip, type, BGMSliderPitch);

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
}
