using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvertisingCanvas : MonoBehaviour
{
    float _time = 0;

    private void Update()
    {
        _time += Time.deltaTime;
        if(_time >= 1.2f)
        {
            SceneConsole.LoadLogin();
        }
    }
}
