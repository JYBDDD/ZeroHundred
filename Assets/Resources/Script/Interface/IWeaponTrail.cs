using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeaponTrail
{
    /// <summary>
    /// TrailRenderer ���� ���� emitting �缳�� �ʿ��� ��쿡�� ȣ�� ����
    /// </summary>
    public abstract void WeaponTrail_UpdateNecessary();

    public abstract void WeaponTrail_UniRx();
}
