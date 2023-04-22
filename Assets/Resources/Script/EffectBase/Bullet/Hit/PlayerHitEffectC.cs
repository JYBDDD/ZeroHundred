using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitEffectC : EffectBaseC
{
    protected override void Awake()
    {
        _pushTime = 2f;
        base.Awake();
    }
}
