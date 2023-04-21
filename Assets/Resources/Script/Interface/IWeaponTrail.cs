using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeaponTrail
{
    /// <summary>
    /// TrailRenderer 삭제 이후 emitting 재설정 필요한 경우에만 호출 권장
    /// </summary>
    public abstract void WeaponTrail_UpdateNecessary();

    public abstract void WeaponTrail_UniRx();
}
