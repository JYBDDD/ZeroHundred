using Path;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletItemC : ItemBase
{
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if (other.gameObject.CompareTag("Player"))
        {
            BulletSpawner.ShootRange -= scriptableObjectC.ShootRangeUp;
            GameManager.Sound.Play(ObjSound_P.ItemGet);
            GameManager.Pool.Push(gameObject);
        }
    }

    protected override void OnTriggerStay(Collider other)
    {
        base.OnTriggerStay(other);
    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
    }
}
