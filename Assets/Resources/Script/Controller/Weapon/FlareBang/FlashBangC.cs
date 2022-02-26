using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashBangC : MonoBehaviour     // PostCameraBloom ��ũ��Ʈ, Boss1Controller���� ȣ��    -> ���� �����
{
    public static bool FlashBangStart;
    float time = 0;

    private void OnEnable()
    {
        FlashBangStart = false;
        time = 0;
    }

    private void Update()
    {
        FlashBangSet();
    }

    private void FlashBangSet()
    {
        time += Time.deltaTime;
        if(time > 1f)
        {
            FlashBangStart = true;
            GameManager.Pool.Push(gameObject);
        }
    }
}
