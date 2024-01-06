using Path;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginSceneButton : MonoBehaviour
{
    [SerializeField]
    Text LoginTextID;               // InputField Login ID ��      
    [SerializeField]
    InputField LoginTextPW;         // InputField Login PW ��

    [SerializeField]
    Text CreateTextID;               // InputField Create ID ��      
    [SerializeField]
    Text CreateTextPW;               // InputField Create PW ��
    [SerializeField]
    Text CreateTextNick;             // InputField Create NickName ��

    private void Awake()
    {
        GameManager.NetworkMain.BackendCallSetting();
    }

    /// <summary>
    /// �α��� �õ�
    /// </summary>
    public void LoginClick()
    {
        GameManager.NetworkMain.SignInLogin(LoginTextID.text,LoginTextPW.text);
        GameManager.Sound.Play(UI_P.UITap);
    }

    /// <summary>
    /// ȸ������ �õ�
    /// </summary>
    public void SignUpClick()
    {
        GameManager.NetworkMain.CustomSignUp(CreateTextID.text, CreateTextPW.text,CreateTextNick.text);
        GameManager.Sound.Play(UI_P.UITap);
    }
}
