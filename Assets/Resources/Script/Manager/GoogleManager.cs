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
    private GameObject LogObejct;   // �α� ������Ʈ
    private Animator logAnim;   // �α� ������Ʈ -> Animator

    private int logOneShot = 0; // Update ���� �ѹ��� ȣ���ϵ���

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
        if (Social.localUser.authenticated == true)     // �̹� ������ �� ���� ���
        {
            BackendReturnObject BRO = Backend.BMember.AuthorizeFederation(GetTokens(), FederationType.Google);

            SceneManager.LoadScene("Scene 1");      // �� �̵�
            GameManager.Sound.Play("Art/Sound/Effect/UI/UISuccess");

        }
        else           // ù ���� �Ͻ�
        {
            Social.localUser.Authenticate((bool success) =>
            {
                // success �� true ��� ȸ�� ����

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
        if (Social.localUser.authenticated == true)     // ���� �α׾ƿ�
        {
            ((PlayGamesPlatform)Social.Active).SignOut();
            GameManager.Sound.Play("Art/Sound/Effect/UI/UITap");
            SceneManager.LoadScene("LoginScene");      // �� �̵�
        }
        else                                            // Ŀ���� �α׾ƿ�
        {
            Backend.BMember.Logout();       // Ŀ���� ���̵�� ������ ��ū�� �����ʽ��ϴ� (��ū �׻� NULL)
            GameManager.Sound.Play("Art/Sound/Effect/UI/UITap");
            SceneManager.LoadScene("LoginScene");      // �� �̵�
        }

    }

    public string GetTokens()
    {
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            // ���� ��ū �ޱ� ù ��° ���
            string _IDtoken = PlayGamesPlatform.Instance.GetIdToken();
            // �� ��° ���
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
