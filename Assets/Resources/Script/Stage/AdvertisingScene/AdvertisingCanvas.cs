using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvertisingCanvas : MonoBehaviour
{
    float _time = 1.2f;

    private void OnEnable()
    {
        TaskManager.Instance.TaskDelayAction(_time, () => { SceneConsole.Instance.LoadLogin(); }).Forget();
    }
}
