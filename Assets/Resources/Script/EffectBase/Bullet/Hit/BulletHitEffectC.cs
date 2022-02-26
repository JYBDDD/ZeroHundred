using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHitEffectC : EffectBaseC
{
    protected float pushTime = 1.5f;

    private void Update()
    {
        if (gameObject.activeSelf)
        {
            _time += Time.deltaTime;
            if (_time > pushTime)
            {
                PushBack();
                _time = 0;
            }
        }
    }

    private void PushBack()
    {
        GameManager.Pool.Push(gameObject);
    }
}
