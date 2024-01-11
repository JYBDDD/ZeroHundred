using Path;
using SceneN;
using UnityEngine;
using UnityEngine.UI;

public class Lobby_ButtonSet : MonoBehaviour
{
    [SerializeField]
    GameObject soundWindow;         // 기본 사운드 윈도우
    [SerializeField]
    GameObject rankWindow;          // ranking 윈도우
    [SerializeField]
    GameObject exitWindow;          // Exit 윈도우
    [SerializeField]
    GameObject muteButton;          // 사운드 윈도우 -> Mute버튼
    [SerializeField]
    GameObject unMuteButton;        // 사운드 윈도우 -> UnMute버튼
    [SerializeField]
    Slider bgmSlider;               // 사운드 윈도우 -> BGM슬라이더
    [SerializeField]
    Slider effSlider;               // 사운드 윈도우 -> Eff슬라이더

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
        if (Time.timeScale == 0)        // GameScene Exit 버튼에서 사용중
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
