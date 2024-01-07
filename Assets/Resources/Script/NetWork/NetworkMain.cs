using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using UnityEngine.UI;
using Path;
using SceneN;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System.Threading;

public class NetworkMain : MonoNetwork
{
    #region Backend
    string rankuuid = "779da490-565d-11ec-a8be-750fdbf90167";       // ��ŷ���̺� uuid ��

    public void BackendCallSetting()             // ���� ù ��ŸƮ�� �ݵ�� ȣ��Ǿ��ϴ� �޼ҵ� (LoginSceneButton ���� ȣ����)
    {
        Backend.Initialize(true);
        GooglePlatformSet();
    }

    public void SignInLogin(string id,string password)      // �α���
    {
        var result = Backend.BMember.CustomLogin(id, password);

        if (result.IsSuccess())
            SceneConsole.Instance.LoadLobby();
        else
            SetCallLogMessage(IMessage.IDPW_Fail);
    }

    // ȸ�� ����
    public void CustomSignUp(string id,string password,string nickName)      
    {
        // �г��ӿ� ���鸸 �ۼ��� ȸ������ ����, �г��� 11���� �ʰ��� ����
        if (nickName.TrimStart() != "" && NicknameRestrictions(nickName))     
        {
            var bro = Backend.BMember.CustomSignUp(id, password);

            if (bro.IsSuccess())
            {
                Backend.BMember.UpdateNickname(nickName);
                SetCallLogMessage(IMessage.C_Create);

                // ���̵� ������ ����� �� �̸� ���� (���� ����)
                Param param = new Param();
                param.Add("UserName", GetNick());
                param.Add("Score", GameManager.SCORE);

                // ���ο Insert
                Backend.GameData.Insert("UserInfo", param);
            }
            else
                SetCallLogMessage(IMessage.ID_Duplicated);
        }
        else
            SetCallLogMessage(IMessage.ID_Blank);
    }

    private bool NicknameRestrictions(string name)          // �г��� 11���� �ʰ� ����
    {
        int stringNum = name.Length;
        if(stringNum > 12)
        {
            SetCallLogMessage(IMessage.Nick_ExcessFail);
            return false;
        }
        return true;
    }

    public string GetNick()
    {
        if(Backend.BMember == null)
        {
            Debug.LogWarning("�߸��� ������ ����Ǽ� �ε����� �ʽ��ϴ�");
            return "��. ��. ��.";
        }

        BackendReturnObject bro = Backend.BMember.GetUserInfo();
        return bro.GetReturnValuetoJSON()["row"]["nickname"].ToString();

    }

    public void ONInsertUserInfo()                          // ������ ����� ��� ReturnWindow ���� �����
    {
        BackendReturnObject bro = Backend.BMember.GetUserInfo();
        Param param = new Param();
        param.Add("UserName", GetNick());
        param.Add("Score", GameManager.SCORE);

        if (bro.IsSuccess())
        {
            BackendReturnObject tt = Backend.GameData.Get("UserInfo", new Where());    // Insert�� ���̺�
            var jsons = tt.GetReturnValuetoJSON();
            var inDate = jsons["rows"][0]["inDate"]["S"].ToString();                   // Insert�� ���̺��� inDate�� (�÷����� �ش� ������ ��/  ��¥?)

            string[] select = {"owner_inDate","Score"};
            BackendReturnObject aOrb = Backend.GameData.GetMyData("UserInfo", inDate, select);
            var aa = aOrb.GetReturnValuetoJSON()["row"]["Score"]["N"].ToString();

            if (int.Parse(aa) < GameManager.SCORE)                                           // ���� ����� �ְ��Ϻ��� ���ٸ� Score ������Ʈ�� ����  
            {
                Backend.GameData.Insert("UserInfo", param);                                  // ���ο Insert
                Backend.URank.User.UpdateUserScore(rankuuid, "UserInfo", inDate, param);     //  ���� Score ������Ʈ
            }
        }
        else
        {
            Debug.Log("���ӵ����� ���� ���� �߻�");
        }
    }

    public int BestMyScore()                // ������ ���� �ְ����� ������
    {
        BackendReturnObject tt = Backend.GameData.Get("UserInfo", new Where());

        var jsons = tt.GetReturnValuetoJSON();

        var inDate = jsons["rows"][0]["inDate"]["S"].ToString();

        string[] select = { "owner_inDate", "Score" };
        BackendReturnObject aOrb = Backend.GameData.GetMyData("UserInfo", inDate, select);
        var aa = aOrb.GetReturnValuetoJSON()["row"]["Score"]["N"].ToString();

        return int.Parse(aa);

    }

    public int BestMyRank()                 // ������ ���� �ְ� ��ŷ�� ������
    {
        if (BestMyScore() == 0)
            return 0;

        BackendReturnObject bro = Backend.URank.User.GetMyRank(rankuuid);

        var rankbro = bro.GetReturnValuetoJSON()["rows"][0]["rank"]["N"].ToString();

        // GetReturnValuetoJSON() �� ã���� ���� -> https://developer.thebackend.io/unity3d/guide/uRanking/user/getMyRank/

        return int.Parse(rankbro);
    }

    public int GetRankListScoreLookUp(int num)      //  ������ �ְ���� 1~10������� ������ ������
    {
        BackendReturnObject bro = Backend.URank.User.GetRankList(rankuuid, 10);
        var listbro = bro.GetReturnValuetoJSON()["rows"][num]["score"]["N"].ToString();

        return int.Parse(listbro);
    }

    public string GetRankListNickLookUp(int num)    // ������ �ְ���� 1~10������� �г����� ������
    {
        BackendReturnObject bro = Backend.URank.User.GetRankList(rankuuid, 10);
        var listbro = bro.GetReturnValuetoJSON()["rows"][num]["nickname"]["S"].ToString();

        return listbro;
    }

    public void GetGoogleHashCode(InputField input)
    {
        string googleHashKey = Backend.Utils.GetGoogleHash();

        if (!string.IsNullOrEmpty(googleHashKey))
        {
            Debug.Log(googleHashKey);
            if (input != null)
                input.text = googleHashKey;
        }
    }

    public void GetMyNewData()      // �� ��ŷ, ���� �����Ͱ� ������ ����         (NickNameGet ���� ������ Awake)
    {
        if(Backend.URank == null)
        {
            Debug.LogWarning("�߸��� ������ ����Ǽ� �ε����� �ʽ��ϴ�");
            return;
        }

        BackendReturnObject bro = Backend.URank.User.GetMyRank(rankuuid);

        if (bro.IsSuccess())
        {
            var rankbro = bro.GetReturnValuetoJSON()["rows"][0]["rank"]["N"].ToString();
            if (rankbro == null | rankbro == "")
                NewLogin();
        }
        else
            NewLogin();

    }

    public void NewLogin()      // ù�α����Ͻ� ����
    {
        Param param = new Param();                  // ���̵� ������ ����� �� �̸� ���� (���� ����)

        Backend.BMember.UpdateNickname(Social.localUser.userName);    // ���� ���̵� = ���� �г���

        param.Add("UserName", GameManager.NetworkMain.GetNick());
        param.Add("Score", GameManager.SCORE);

        Backend.GameData.Insert("UserInfo", param);                                  // ���ο Insert

    }
    #endregion

    #region Google Platform
    /// <summary>
    /// ���� �÷��� �ʱ� ����
    /// </summary>
    public void GooglePlatformSet()
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
    }

    public void GoogleLogin()
    {
        // �̹� ������ �� ���� ���
        if (Social.localUser.authenticated == true)
        {
            BackendReturnObject BRO = Backend.BMember.AuthorizeFederation(GoogleGetTokens(), FederationType.Google);

            // �� �̵�
            SceneConsole.Instance.LoadScene(SceneName.LobbyScene);
        }
        // ù ���� �Ͻ�
        else
        {
            Social.localUser.Authenticate((bool success) =>
            {
                // success �� true ��� ȸ�� ����
                if (!success)
                    SetCallLogMessage(IMessage.G_Login_Fail);
            });
        }

    }

    public void GoogleLogOut()
    {
        if (Social.localUser.authenticated == true)     // ���� �α׾ƿ�
        {
            ((PlayGamesPlatform)Social.Active).SignOut();
            //GameManager.Sound.Play(UI_P.UITap);
            SceneConsole.Instance.LoadScene(SceneName.LoginScene);
        }
        else                                            // Ŀ���� �α׾ƿ�
        {
            Backend.BMember.Logout();       // Ŀ���� ���̵�� ������ ��ū�� �����ʽ��ϴ� (��ū �׻� NULL)
            //GameManager.Sound.Play(UI_P.UITap);
            SceneConsole.Instance.LoadScene(SceneName.LoginScene);
        }

    }

    public string GoogleGetTokens()
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
    #endregion
}
