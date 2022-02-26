using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginBGMBox : MonoBehaviour
{
    private void Start()
    {
        GameManager.Sound.Play("Art/Sound/BGM/LoadingScene_BGM", Define.Sound.bgm);
    }
}
