using Path;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginBGMBox : MonoBehaviour
{
    private void Start()
    {
        GameManager.Sound.Play(SceneSound_P.StartSceneBGM, Define.Sound.bgm);
    }
}
