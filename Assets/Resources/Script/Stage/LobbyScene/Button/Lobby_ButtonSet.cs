using Path;
using SceneN;
using UnityEngine;
using UnityEngine.UI;

public class Lobby_ButtonSet : MonoBehaviour
{
    [SerializeField]
    GameObject soundWindow;         // �⺻ ���� ������
    [SerializeField]
    GameObject rankWindow;          // ranking ������
    [SerializeField]
    GameObject exitWindow;          // Exit ������
    [SerializeField]
    GameObject muteButton;          // ���� ������ -> Mute��ư
    [SerializeField]
    GameObject unMuteButton;        // ���� ������ -> UnMute��ư
    [SerializeField]
    Slider bgmSlider;               // ���� ������ -> BGM�����̴�
    [SerializeField]
    Slider effSlider;               // ���� ������ -> Eff�����̴�

    #region Next Stage
    public void NextStartButton()
    {
        GameManager.Pool.Clear();
        GameManager.Sound.Clear();
        SceneConsole.Instance.LoadGame();
    }
    #endregion

    #region Sound
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
        var s = GameManager.Sound;
        s.SetMute(1);
        s.SetBGMVal(0);
        s.SetSFXVal(0);
        bgmSlider.value = s.GetBGMF();
        effSlider.value = s.GetSFXF();

    }

    public void UnMute()
    {
        var s = GameManager.Sound;
        s.SetMute(0);
        s.SetBGMVal(0.5f);
        s.SetSFXVal(0.5f);
        bgmSlider.value = s.GetBGMF();
        effSlider.value = s.GetSFXF();
        GameManager.Sound.Play(UI_P.UITap);
    }
    #endregion

    #region Rank
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
    #endregion

    #region Exit
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
    #endregion
}
