using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class FlashBangC : MonoBehaviour     // PostCameraBloom ��ũ��Ʈ, Boss1Controller���� ȣ��    -> ���� �����
{
    public static bool FlashBangStart;
    float time = 0;

    private void Awake()
    {
        this.UpdateAsObservable().Where(_ => time < 1.1f).Subscribe(_ => FlashBangSet());
    }

    private void OnEnable()
    {
        FlashBangStart = false;
        time = 0;
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
