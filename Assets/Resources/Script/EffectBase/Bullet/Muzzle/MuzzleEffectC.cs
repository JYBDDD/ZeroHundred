using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleEffectC : EffectBaseC
{
    protected override void Awake()
    {
        _pushTime = 0.3f;
        base.Awake();
    }
}
