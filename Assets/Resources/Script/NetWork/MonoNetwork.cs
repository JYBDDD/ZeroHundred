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

    [SerializeField, Header("�α��� ������")]
    GameObject _loginWindow;
    [SerializeField, Header("ȸ������ ������")]
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
    /// �α� ����
    /// </summary>
    /// <param name="setLog"></param>
    public void SetCallLogMessage(IMessage setLog)
    {
        CurrentCallMessage = setLog;
    }

    /// <summary>
    /// �޼��� Ÿ�Կ� ���� �α� ����
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
    /// ���� �α��� ����
    /// </summary>
    protected void GoogleLoginFail()
    {
        GetLogText().text = "���� �α��� ����";
        _logAnim.SetTrigger("SetLog");
        GameManager.Sound.Play(UI_P.UIFail);

        DelayWindow();
    }

    /// <summary>
    /// Ŀ���� ���� ������ ���̵� �ߺ� (����)
    /// </summary>
    protected void CustomCreateDuplicated()
    {
        GetLogText().text = "�ش� ���̵� �̹� �����մϴ�.";
        _logAnim.SetTrigger("SetLog");
        GameManager.Sound.Play(UI_P.UIFail);

        DelayWindow();
    }

    /// <summary>
    /// ���̵� ������ �����Ͽ� ���� ����
    /// </summary>
    protected void CustomCreateSpaceInId()
    {
        GetLogText().text = "�ùٸ� ���������� �ƴմϴ�!";
        _logAnim.SetTrigger("SetLog");
        GameManager.Sound.Play(UI_P.UIFail);
    }

    /// <summary>
    /// Ŀ���� �α����� ID or Password Ʋ�� (����)
    /// </summary>
    protected void CustomLoginIdOrPasswordFail()
    {
        GetLogText().text = "ID or Pw�� �ùٸ��� �ʽ��ϴ�.";
        _logAnim.SetTrigger("SetLog");
        GameManager.Sound.Play(UI_P.UIFail);

        DelayWindow();
    }

    /// <summary>
    /// Ŀ���� ���� ������ �г��� �ִ� ������ �ʰ� (����)
    /// </summary>
    protected void CustomCreateExcessOfNick()
    {
        GetLogText().text = "�г��� ������ �ʰ�!";
        _logAnim.SetTrigger("SetLog");
        GameManager.Sound.Play(UI_P.UIFail);

        DelayWindow();
    }

    /// <summary>
    /// Ŀ���� ���� ���� �Ϸ�
    /// </summary>
    protected void CustomIdCreateComplete()
    {
        GetLogText().text = "Ŀ���� ���� ���� �Ϸ�!";
        _logAnim.SetTrigger("SetLog");
        GameManager.Sound.Play(UI_P.UISuccess);

        DelayWindow();
    }

    /// <summary>
    /// ���� �ð� ���� ������ �缳��
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
    /// ȸ������ ������ ����
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
    G_Login_Fail,   // ���� �α��� ����
    ID_Duplicated,  // ID �ߺ�
    ID_Blank,       // ID ���� ����
    IDPW_Fail,      // ID or PW ����
    Nick_ExcessFail,  // �г��� �ִ� ���� �ʰ�
    C_Create,       // Ŀ���� ���� ���� ����
}
