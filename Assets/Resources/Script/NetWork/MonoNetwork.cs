using Path;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonoNetwork : MonoBehaviour
{
    Animator _logAnim;
    Text _logText;

    [SerializeField, Header("로그인 윈도우")]
    GameObject _loginWindow;
    [SerializeField, Header("회원가입 윈도우")]
    GameObject _joinWindow;

    static IMessage CurrentCallMessage = IMessage.None;

    Text GetLogText()
    {
        if (_logAnim == null)
            _logAnim = gameObject.GetComponentInChildren<Animator>();

        if (_logText == null)
            _logText = _logAnim.GetComponentInChildren<Text>();

        return _logText;
    }

    private void Update()
    {
        LogMessageExcute();
    }

    /// <summary>
    /// 로그 설정
    /// </summary>
    /// <param name="setLog"></param>
    public void SetCallLogMessage(IMessage setLog)
    {
        CurrentCallMessage = setLog;
    }

    /// <summary>
    /// 메세지 타입에 따른 로그 실행
    /// </summary>
    void LogMessageExcute()
    {
        if (CurrentCallMessage != IMessage.None)
        {
            switch (CurrentCallMessage)
            {
                case IMessage.None:
                    break;
                case IMessage.G_Login_Fail:
                    GoogleLoginFail();
                    break;
                case IMessage.ID_Duplicated:
                    CustomCreateDuplicated();
                    break;
                case IMessage.ID_Blank:
                    CustomCreateSpaceInId();
                    break;
                case IMessage.IDPW_Fail:
                    CustomLoginIdOrPasswordFail();
                    break;
                case IMessage.Nick_ExcessFail:
                    CustomCreateExcessOfNick();
                    break;
                case IMessage.C_Create:
                    CustomIdCreateComplete();
                    break;
            }

            CurrentCallMessage = IMessage.None;
        }
    }

    /// <summary>
    /// 구글 로그인 실패
    /// </summary>
    protected void GoogleLoginFail()
    {
        GetLogText().text = "구글 로그인 실패";
        _logAnim.SetTrigger("SetLog");
        GameManager.Sound.Play(UI_P.UIFail);

        DelayWindow();
    }

    /// <summary>
    /// 커스텀 계정 생성중 아이디 중복 (실패)
    /// </summary>
    protected void CustomCreateDuplicated()
    {
        GetLogText().text = "해당 아이디가 이미 존재합니다.";
        _logAnim.SetTrigger("SetLog");
        GameManager.Sound.Play(UI_P.UIFail);

        DelayWindow();
    }

    /// <summary>
    /// 아이디에 공백이 존재하여 생성 실패
    /// </summary>
    protected void CustomCreateSpaceInId()
    {
        GetLogText().text = "올바른 계정정보가 아닙니다!";
        _logAnim.SetTrigger("SetLog");
        GameManager.Sound.Play(UI_P.UIFail);
    }

    /// <summary>
    /// 커스텀 로그인중 ID or Password 틀림 (실패)
    /// </summary>
    protected void CustomLoginIdOrPasswordFail()
    {
        GetLogText().text = "ID or Pw가 올바르지 않습니다.";
        _logAnim.SetTrigger("SetLog");
        GameManager.Sound.Play(UI_P.UIFail);

        DelayWindow();
    }

    /// <summary>
    /// 커스텀 계정 생성중 닉네임 최대 허용길이 초과 (실패)
    /// </summary>
    protected void CustomCreateExcessOfNick()
    {
        GetLogText().text = "닉네임 허용길이 초과!";
        _logAnim.SetTrigger("SetLog");
        GameManager.Sound.Play(UI_P.UIFail);

        DelayWindow();
    }

    /// <summary>
    /// 커스텀 계정 생성 완료
    /// </summary>
    protected void CustomIdCreateComplete()
    {
        GetLogText().text = "커스텀 계정 생성 완료!";
        _logAnim.SetTrigger("SetLog");
        GameManager.Sound.Play(UI_P.UISuccess);

        DelayWindow();
    }

    /// <summary>
    /// 일정 시간 이후 윈도우 재설정
    /// </summary>
    void DelayWindow()
    {
        TaskManager.Instance.TaskDelayAction(1.5f, () =>
         {
             _loginWindow.SetActive(true);
             _joinWindow.SetActive(false);
         }).Forget();
    }

    /// <summary>
    /// 회원가입 윈도우 오픈
    /// </summary>
    public void JoinWindowOpen()
    {
        _loginWindow.SetActive(false);
        _joinWindow.SetActive(true);
        GameManager.Sound.Play(UI_P.UITap);
    }
}


public enum IMessage
{
    None,
    G_Login_Fail,   // 구글 로그인 실패
    ID_Duplicated,  // ID 중복
    ID_Blank,       // ID 공백 존재
    IDPW_Fail,      // ID or PW 오류
    Nick_ExcessFail,  // 닉네임 최대 길이 초과
    C_Create,       // 커스텀 계정 생성 성공
}
