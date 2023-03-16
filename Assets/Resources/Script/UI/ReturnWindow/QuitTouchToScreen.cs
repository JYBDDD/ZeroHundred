using SceneN;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitTouchToScreen : MonoBehaviour
{
    public void QuitToLobby()
    {
        SceneManager.LoadScene(SceneName.LobbyScene);
    }
}
