using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using UnityEngine.UI;
using Path;
using SceneN;

public class BackendMain
{
    string rankuuid = "779da490-565d-11ec-a8be-750fdbf90167";       // ��ŷ���̺� uuid ��

    public void BackendCallSetting()             // ���� ù ��ŸƮ�� �ݵ�� ȣ��Ǿ��ϴ� �޼ҵ� (LoginSceneButton ���� ȣ����)
    {
        Backend.Initialize(true);
    }

    public void SignInLogin(string id,string password)      // �α���
    {
        var result = Backend.BMember.CustomLogin(id, password);

        if(result.IsSuccess())
        {
            SceneConsole.LoadLobby();
            GameManager.Sound.Play(UI_P.UISuccess);
        }
        else
        {
            LoginSceneButton.GUILoginErrorBool = true;
            GameManager.Sound.Play(UI_P.UIFail);
        }
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
                LoginSceneButton.GUICreateSuccessBool = true;
                GameManager.Sound.Play(UI_P.UISuccess);

                // ���̵� ������ ����� �� �̸� ���� (���� ����)
                Param param = new Param();                  
                param.Add("UserName", GetNick());
                param.Add("Score", GameManager.SCORE);

                // ���ο Insert
                Backend.GameData.Insert("UserInfo", param);                                  
            }
            else
            {
                LoginSceneButton.GUICreateErrorBool = true;
                GameManager.Sound.Play(UI_P.UIFail);
            }
        }
        else
        {
            LoginSceneButton.GUICreateBlankBool = true;
            GameManager.Sound.Play(UI_P.UIFail);
        }

    }

    private bool NicknameRestrictions(string name)          // �г��� 11���� �ʰ� ����
    {
        int stringNum = name.Length;
        if(stringNum > 12)
        {
            LoginSceneButton.GUICreateNickBool = true;
            return false;
        }
        return true;
    }

    public string GetNick()
    {
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

        param.Add("UserName", GameManager.BackendMain.GetNick());
        param.Add("Score", GameManager.SCORE);

        Backend.GameData.Insert("UserInfo", param);                                  // ���ο Insert

    }
}
