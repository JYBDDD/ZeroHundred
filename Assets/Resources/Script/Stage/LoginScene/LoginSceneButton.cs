using Path;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginSceneButton : MonoBehaviour
{
    float time = 0;

    [SerializeField]
    GameObject LoginWindow;
    [SerializeField]
    Text LoginTextID;               // InputField Login ID 값      
    [SerializeField]
    InputField LoginTextPW;         // InputField Login PW 값
    public static bool GUILoginErrorBool = false;


    [SerializeField]
    GameObject NewSignUpWindow;
    [SerializeField]
    Text CreateTextID;               // InputField Create ID 값      
    [SerializeField]
    Text CreateTextPW;               // InputField Create PW 값
    [SerializeField]
    Text CreateTextNick;             // InputField Create NickName 값
    public static bool GUICreateErrorBool = false;
    public static bool GUICreateSuccessBool = false;
    public static bool GUICreateBlankBool = false;
    public static bool GUICreateNickBool = false;

    GUIStyle guiStyle = new GUIStyle();         // 에러메세지 스타일 설정

    public void LoginClick()
    {
        GameManager.BackendMain.SignInLogin(LoginTextID.text,LoginTextPW.text);
        GameManager.Sound.Play(UI_P.UITap);
        // 해당 아이디 및 비밀번호가 맞다면 다음씬으로 이동

    }

    public void NewClick()
    {
        LoginWindow.SetActive(false);
        NewSignUpWindow.SetActive(true);
        GameManager.Sound.Play(UI_P.UITap);
    }

    public void SignUpClick()
    {
        GameManager.BackendMain.CustomSignUp(CreateTextID.text, CreateTextPW.text,CreateTextNick.text);
        GameManager.Sound.Play(UI_P.UITap);
    }

    private void Awake()
    {
        GameManager.BackendMain.BackendCallSetting();
        guiStyle.fontSize = 50;
    }

    private void Update()
    {
        GUIOn();
    }

    private void GUIOn()        // GUI 메세지 호출 1.5초후 비활성화
    {
        if(GUILoginErrorBool == true | GUICreateErrorBool == true | GUICreateSuccessBool == true | GUICreateBlankBool == true | GUICreateNickBool == true)
        {
            time += Time.deltaTime;
            if(time > 1.5f)
            {
                time = 0;
                GUILoginErrorBool = false;
                GUICreateErrorBool = false;
                GUICreateSuccessBool = false;
                GUICreateBlankBool = false;
                GUICreateNickBool = false;

                LoginWindow.SetActive(true);
                NewSignUpWindow.SetActive(false);
            }
        }
    }

    public void OnGUI()            // 아이디 오류나 생성실패시 사용되도록 설정
    {
        if (GUILoginErrorBool == true)
            GUI.Box(new Rect(50, 400, Screen.width, Screen.height), "ID or password Error",guiStyle);           // 로그인  - 아이디 및 패스워드 오류
        if(GUICreateErrorBool == true)
            GUI.Box(new Rect(50, 400, Screen.width, Screen.height), "ID is Duplicated", guiStyle);               // 아이디 생성 - 중복
        if (GUICreateSuccessBool == true)
            GUI.Box(new Rect(50, 400, Screen.width, Screen.height), "ID creation complete", guiStyle);           // 아이디 생성 - 완료
        if (GUICreateBlankBool == true)
            GUI.Box(new Rect(50, 400, Screen.width, Screen.height), "Remove Spaces in Nickname", guiStyle);      // 아이디 생성 - 닉네임 공백 존재
        if (GUICreateNickBool == true)
            GUI.Box(new Rect(50, 450, Screen.width, Screen.height), "Nickname Exceeds 11 Character", guiStyle);  // 아이디 생성 - 닉네임 11글자 초과
    }
}
