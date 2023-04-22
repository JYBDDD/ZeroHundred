using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHitEffectC : EffectBaseC
{
    protected override void Awake()
    {
        _pushTime = 1.5f;
        base.Awake();
    }
}
