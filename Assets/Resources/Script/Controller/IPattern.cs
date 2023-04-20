using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPattern
{
    public void Pattern();

    public void WeaponChange(Define.Weapon weapon);
}

public interface IHit
{
    public void Hit(Action action);
}
