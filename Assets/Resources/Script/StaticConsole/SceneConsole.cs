using SceneN;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneConsole
{
    public static void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public static void LoadLobby()
    {
        LoadScene(SceneName.LobbyScene);
    }
    public static void LoadGame()
    {
        LoadScene(SceneName.GameScene);
    }
    public static void LoadLogin()
    {
        LoadScene(SceneName.LoginScene);
    }
}
