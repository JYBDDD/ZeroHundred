using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawnerBase : MonoBehaviour
{
    protected string clipPath = string.Empty;
    protected string muzzlePath = string.Empty;
    protected string projectilePath = string.Empty;

    protected Transform enemyB_P;
    protected Transform muzzleH_P;
    protected Transform playerB_P;

    protected Quaternion lookQut = Quaternion.identity;
    protected Quaternion identity = Quaternion.identity;

    [SerializeField]
    protected float shootRange = 0;
    [SerializeField]
    protected float shootCount = 0;

    protected virtual void Awake()
    {
        enemyB_P = GameManager.EnemyBulletParent.transform;
        muzzleH_P = GameManager.MuzzleOfHitParent.transform;
        playerB_P = GameManager.PlayerBulletParent.transform;
    }
}
