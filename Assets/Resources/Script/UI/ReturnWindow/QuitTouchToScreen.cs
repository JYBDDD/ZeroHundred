using SceneN;
using UnityEngine;


public class QuitTouchToScreen : MonoBehaviour
{
    public void QuitToLobby()
    {
        SceneConsole.Instance.LoadLobby();
    }
}
