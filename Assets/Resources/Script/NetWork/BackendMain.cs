using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using UnityEngine.UI;
using Path;
using SceneN;

public class BackendMain
{
    string rankuuid = "779da490-565d-11ec-a8be-750fdbf90167";       // 랭킹테이블 uuid 값

    public void BackendCallSetting()             // 게임 첫 스타트시 반드시 호출되야하는 메소드 (LoginSceneButton 에서 호출중)
    {
        Backend.Initialize(true);
    }

    public void SignInLogin(string id,string password)      // 로그인
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

    // 회원 가입
    public void CustomSignUp(string id,string password,string nickName)      
    {
        // 닉네임에 공백만 작성시 회원가입 실패, 닉네임 11글자 초과시 실패
        if (nickName.TrimStart() != "" && NicknameRestrictions(nickName))     
        {
            var bro = Backend.BMember.CustomSignUp(id, password);

            if (bro.IsSuccess())
            {
                Backend.BMember.UpdateNickname(nickName);
                LoginSceneButton.GUICreateSuccessBool = true;
                GameManager.Sound.Play(UI_P.UISuccess);

                // 아이디 생성시 사용할 값 미리 적용 (오류 방지)
                Param param = new Param();                  
                param.Add("UserName", GetNick());
                param.Add("Score", GameManager.SCORE);

                // 새로운값 Insert
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

    private bool NicknameRestrictions(string name)          // 닉네임 11글자 초과 방지
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

    public void ONInsertUserInfo()                          // 마지막 기록을 담는 ReturnWindow 에서 사용중
    {
        BackendReturnObject bro = Backend.BMember.GetUserInfo();
        Param param = new Param();
        param.Add("UserName", GetNick());
        param.Add("Score", GameManager.SCORE);

        if (bro.IsSuccess())
        {
            BackendReturnObject tt = Backend.GameData.Get("UserInfo", new Where());    // Insert할 테이블값
            var jsons = tt.GetReturnValuetoJSON();
            var inDate = jsons["rows"][0]["inDate"]["S"].ToString();                   // Insert할 테이블의 inDate값 (플레이한 해당 유저의 값/  날짜?)

            string[] select = {"owner_inDate","Score"};
            BackendReturnObject aOrb = Backend.GameData.GetMyData("UserInfo", inDate, select);
            var aa = aOrb.GetReturnValuetoJSON()["row"]["Score"]["N"].ToString();

            if (int.Parse(aa) < GameManager.SCORE)                                           // 현재 기록이 최고기록보다 높다면 Score 업데이트를 실행  
            {
                Backend.GameData.Insert("UserInfo", param);                                  // 새로운값 Insert
                Backend.URank.User.UpdateUserScore(rankuuid, "UserInfo", inDate, param);     //  유저 Score 업데이트
            }
        }
        else
        {
            Debug.Log("게임데이터 저장 오류 발생");
        }
    }

    public int BestMyScore()                // 서버의 나의 최고기록을 가져옴
    {
        BackendReturnObject tt = Backend.GameData.Get("UserInfo", new Where());

        var jsons = tt.GetReturnValuetoJSON();

        var inDate = jsons["rows"][0]["inDate"]["S"].ToString();

        string[] select = { "owner_inDate", "Score" };
        BackendReturnObject aOrb = Backend.GameData.GetMyData("UserInfo", inDate, select);
        var aa = aOrb.GetReturnValuetoJSON()["row"]["Score"]["N"].ToString();

        return int.Parse(aa);

    }

    public int BestMyRank()                 // 서버의 나의 최고 랭킹을 가져옴
    {
        if (BestMyScore() == 0)
            return 0;

        BackendReturnObject bro = Backend.URank.User.GetMyRank(rankuuid);

        var rankbro = bro.GetReturnValuetoJSON()["rows"][0]["rank"]["N"].ToString();

        // GetReturnValuetoJSON() 값 찾을때 참고 -> https://developer.thebackend.io/unity3d/guide/uRanking/user/getMyRank/

        return int.Parse(rankbro);
    }

    public int GetRankListScoreLookUp(int num)      //  서버의 최고순위 1~10등까지의 점수를 가져옴
    {
        BackendReturnObject bro = Backend.URank.User.GetRankList(rankuuid, 10);
        var listbro = bro.GetReturnValuetoJSON()["rows"][num]["score"]["N"].ToString();

        return int.Parse(listbro);
    }

    public string GetRankListNickLookUp(int num)    // 서버의 최고순위 1~10등까지의 닉네임을 가져옴
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

    public void GetMyNewData()      // 내 랭킹, 점수 데이터가 없을시 실행         (NickNameGet 에서 실행중 Awake)
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

    public void NewLogin()      // 첫로그인일시 실행
    {
        Param param = new Param();                  // 아이디 생성시 사용할 값 미리 적용 (오류 방지)

        Backend.BMember.UpdateNickname(Social.localUser.userName);    // 구글 아이디 = 게임 닉네임

        param.Add("UserName", GameManager.BackendMain.GetNick());
        param.Add("Score", GameManager.SCORE);

        Backend.GameData.Insert("UserInfo", param);                                  // 새로운값 Insert

    }
}
