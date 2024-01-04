using Path;
using SceneN;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSet : MonoBehaviour
{
    public void NextStartButton()
    {
        GameManager.Pool.Clear();
        GameManager.Sound.Clear();
        SceneConsole.Instance.LoadGame();
    }

    [SerializeField]
    GameObject soundWindow;         // �⺻ ���� ������
    [SerializeField]
    GameObject muteButton;          // ���� ������ -> Mute��ư
    [SerializeField]
    GameObject unMuteButton;        // ���� ������ -> UnMute��ư
    [SerializeField]
    Slider bgmSlider;               // ���� ������ -> BGM�����̴�
    [SerializeField]
    Slider effSlider;               // ���� ������ -> Eff�����̴�

    public void SoundButtonClick()
    {
        soundWindow.SetActive(true);
        GameManager.Sound.Play(UI_P.UITap);
    }

    public void SoundButtonClickExit()
    {
        if (Time.timeScale == 0)        // GameScene Exit ��ư���� �����
            Time.timeScale = 1;

        soundWindow.SetActive(false);
        GameManager.Sound.Play(UI_P.UITap);
    }

    public void Mute()
    {
        SoundManager.BGMSliderPitch = 0f;
        SoundManager.EffSliderPitch = 0f;
        bgmSlider.value = 0f;
        effSlider.value = 0f;
    }

    public void UnMute()
    {
        SoundManager.BGMSliderPitch = 1f;
        SoundManager.EffSliderPitch = 1f;
        bgmSlider.value = 1f;
        effSlider.value = 1f;
        GameManager.Sound.Play(UI_P.UITap);
    }

    [SerializeField]
    GameObject rankWindow;          // ranking ������

    public void RankButtonClick()
    {
        rankWindow.SetActive(true);
        GameManager.Sound.Play(UI_P.UITap);
    }

    public void RankExitButton()
    {
        rankWindow.SetActive(false);
        GameManager.Sound.Play(UI_P.UITap);
    }


    [SerializeField]
    GameObject exitWindow;          // Exit ������

    public void ExitButtonClick()
    {
        exitWindow.SetActive(true);
        GameManager.Sound.Play(UI_P.UITap);
    }

    public void YesExitClick()
    {
        Application.Quit();
    }

    public void NoExitClick()
    {
        exitWindow.SetActive(false);
        GameManager.Sound.Play(UI_P.UITap);
    }
}
