using SceneN;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.UI;

public class SceneConsole : MonoSingleton<SceneConsole>
{
    CanvasGroup _group;

    public void LoadScene(string name)
    {
        InitGroup();

        AsyncSceneLoad_FadeInOut(name).Forget();
    }

    public  void LoadLobby()
    {
        LoadScene(SceneName.LobbyScene);
    }
    public void LoadGame()
    {
        LoadScene(SceneName.GameScene);
    }
    public void LoadLogin()
    {
        LoadScene(SceneName.LoginScene);
    }

    /// <summary>
    /// 캔버스 그룹 설정
    /// </summary>
    void InitGroup()
    {
        if(_group == null)
        {
            var canvas = gameObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = 1000;
            var img = gameObject.AddComponent<Image>();
            img.color = Color.black;
            _group = canvas.gameObject.AddComponent<CanvasGroup>();
            _group.alpha = 0;
            DontDestroyOnLoad(gameObject);
        }
    }

    /// <summary>
    /// 비동기 씬로드 +(FadeIn/Out)
    /// </summary>
    /// <param name="sceneName"></param>
    /// <returns></returns>
    private async UniTaskVoid AsyncSceneLoad_FadeInOut(string sceneName)
    {
        // Fade In
        while(_group.alpha < 1)
        {
            await UniTask.Yield();
            _group.alpha += Time.deltaTime * 0.8f;
        }

        CancellationTokenSource tokenS = new CancellationTokenSource();
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        op.allowSceneActivation = false;

        await UniTask.WaitUntil(() => op.isDone == false);

        op.allowSceneActivation = true;

        // Fade Out
        while (_group.alpha > 0)
        {
            await UniTask.Yield();
            _group.alpha -= Time.deltaTime * 0.8f;
        }

        TaskManager.Instance.TaskOff(tokenS);
    }
}
