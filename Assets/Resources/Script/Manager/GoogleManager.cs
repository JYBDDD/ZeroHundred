using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using UnityEngine.UI;
using BackEnd;
using UnityEngine.SceneManagement;
using GooglePlayGames.BasicApi;

public class GoogleManager : MonoBehaviour
{
    [SerializeField]
    private Text LogText;

    [SerializeField]
    private GameObject LogObejct;   // 로그 오브젝트
    private Animator logAnim;   // 로그 오브젝트 -> Animator

    private int logOneShot = 0; // Update 에서 한번만 호출하도록

    [SerializeField]
    private GameObject LogOutWindow;

    private void Start()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration
        .Builder()
        .RequestServerAuthCode(false)
        .RequestEmail()
        .RequestIdToken()
        .Build();

        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = false;

        PlayGamesPlatform.Activate();

        if (LogObejct != null)
            logAnim = LogObejct.GetComponent<Animator>();
        if (LogOutWindow != null)
            LogOutWindow.SetActive(false);
    }

    private void Update()
    {
        if(logOneShot <= 0)
        {
            logAnim.SetBool("SetLog", false);
            logOneShot++;
        }
    }

    public void Login()
    {
        if (Social.localUser.authenticated == true)     // 이미 가입이 된 상태 라면
        {
            BackendReturnObject BRO = Backend.BMember.AuthorizeFederation(GetTokens(), FederationType.Google);

            SceneManager.LoadScene("Scene 1");      // 씬 이동
            GameManager.Sound.Play("Art/Sound/Effect/UI/UISuccess");

        }
        else           // 첫 가입 일시
        {
            Social.localUser.Authenticate((bool success) =>
            {
                // success 가 true 라면 회원 가입

                if (!success)
                {
                    LogText.text = "Google Login Fail";
                    GameManager.Sound.Play("Art/Sound/Effect/UI/UIFail");
                    logAnim.SetBool("SetLog", true);
                    logOneShot = 0;
                }
            });
        }
        
    }

    public void OpenLogOutWindow()
    {
        LogOutWindow.SetActive(true);
    }

    public void YesLogOut()
    {
        LogOut();
    }

    public void NoLogOut()
    {
        LogOutWindow.SetActive(false);
    }

    public void LogOut()
    {
        if (Social.localUser.authenticated == true)     // 구글 로그아웃
        {
            ((PlayGamesPlatform)Social.Active).SignOut();
            GameManager.Sound.Play("Art/Sound/Effect/UI/UITap");
            SceneManager.LoadScene("LoginScene");      // 씬 이동
        }
        else                                            // 커스텀 로그아웃
        {
            Backend.BMember.Logout();       // 커스텀 아이디는 별도로 토큰을 받지않습니다 (토큰 항상 NULL)
            GameManager.Sound.Play("Art/Sound/Effect/UI/UITap");
            SceneManager.LoadScene("LoginScene");      // 씬 이동
        }

    }

    public string GetTokens()
    {
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            // 유저 토큰 받기 첫 번째 방법
            string _IDtoken = PlayGamesPlatform.Instance.GetIdToken();
            // 두 번째 방법
            //string _IDtoken = ((PlayGamesLocalUser)Social.localUser).GetIdToken();
            return _IDtoken;
        }
        else
        {
            //Debug.Log("Not Connected : fail");
            return null;
        }
    }
}
