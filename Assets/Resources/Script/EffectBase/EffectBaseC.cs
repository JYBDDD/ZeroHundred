using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class EffectBaseC : MonoBehaviour
{
    protected float _time = 0.0f;
    protected float _pushTime = 0;

    protected virtual void Awake()
    {
        CheckTime_UniRx();
    }

    private void CheckTime_UniRx()
    {
        this.UpdateAsObservable().Subscribe(_ => PushBack());
    }

    private void PushBack()
    {
        _time += Time.deltaTime;

        if (_time > _pushTime)
        {
            GameManager.Pool.Push(gameObject);
            _time = 0;
        }

    }
}
