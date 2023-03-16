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
    Text LoginTextID;               // InputField Login ID ��      
    [SerializeField]
    InputField LoginTextPW;         // InputField Login PW ��
    public static bool GUILoginErrorBool = false;


    [SerializeField]
    GameObject NewSignUpWindow;
    [SerializeField]
    Text CreateTextID;               // InputField Create ID ��      
    [SerializeField]
    Text CreateTextPW;               // InputField Create PW ��
    [SerializeField]
    Text CreateTextNick;             // InputField Create NickName ��
    public static bool GUICreateErrorBool = false;
    public static bool GUICreateSuccessBool = false;
    public static bool GUICreateBlankBool = false;
    public static bool GUICreateNickBool = false;

    GUIStyle guiStyle = new GUIStyle();         // �����޼��� ��Ÿ�� ����

    public void LoginClick()
    {
        GameManager.BackendMain.SignInLogin(LoginTextID.text,LoginTextPW.text);
        GameManager.Sound.Play(UI_P.UITap);
        // �ش� ���̵� �� ��й�ȣ�� �´ٸ� ���������� �̵�

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

    private void GUIOn()        // GUI �޼��� ȣ�� 1.5���� ��Ȱ��ȭ
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

    public void OnGUI()            // ���̵� ������ �������н� ���ǵ��� ����
    {
        if (GUILoginErrorBool == true)
            GUI.Box(new Rect(50, 400, Screen.width, Screen.height), "ID or password Error",guiStyle);           // �α���  - ���̵� �� �н����� ����
        if(GUICreateErrorBool == true)
            GUI.Box(new Rect(50, 400, Screen.width, Screen.height), "ID is Duplicated", guiStyle);               // ���̵� ���� - �ߺ�
        if (GUICreateSuccessBool == true)
            GUI.Box(new Rect(50, 400, Screen.width, Screen.height), "ID creation complete", guiStyle);           // ���̵� ���� - �Ϸ�
        if (GUICreateBlankBool == true)
            GUI.Box(new Rect(50, 400, Screen.width, Screen.height), "Remove Spaces in Nickname", guiStyle);      // ���̵� ���� - �г��� ���� ����
        if (GUICreateNickBool == true)
            GUI.Box(new Rect(50, 450, Screen.width, Screen.height), "Nickname Exceeds 11 Character", guiStyle);  // ���̵� ���� - �г��� 11���� �ʰ�
    }
}
