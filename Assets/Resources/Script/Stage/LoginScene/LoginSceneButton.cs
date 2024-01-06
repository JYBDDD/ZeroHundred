using Path;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginSceneButton : MonoBehaviour
{
    [SerializeField]
    Text LoginTextID;               // InputField Login ID 값      
    [SerializeField]
    InputField LoginTextPW;         // InputField Login PW 값

    [SerializeField]
    Text CreateTextID;               // InputField Create ID 값      
    [SerializeField]
    Text CreateTextPW;               // InputField Create PW 값
    [SerializeField]
    Text CreateTextNick;             // InputField Create NickName 값

    private void Awake()
    {
        GameManager.NetworkMain.BackendCallSetting();
    }

    /// <summary>
    /// 로그인 시도
    /// </summary>
    public void LoginClick()
    {
        GameManager.NetworkMain.SignInLogin(LoginTextID.text,LoginTextPW.text);
        GameManager.Sound.Play(UI_P.UITap);
    }

    /// <summary>
    /// 회원가입 시도
    /// </summary>
    public void SignUpClick()
    {
        GameManager.NetworkMain.CustomSignUp(CreateTextID.text, CreateTextPW.text,CreateTextNick.text);
        GameManager.Sound.Play(UI_P.UITap);
    }
}
