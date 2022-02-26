using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBombingOn : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        StartCoroutine(BombingAttack());
    }

    private void OnDisable()
    {
        StopCoroutine(BombingAttack());
    }

    IEnumerator BombingAttack()
    {
        yield return null;

        bool isbool = true;
        if(isbool)
        {
            yield return new WaitForSeconds(1f);
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(0.1f);
            GameManager.Resource.Instantiate("Weapon/Bombing/BombingEffect",transform.position,Quaternion.identity,GameManager.EnemyBulletParent.transform);
            isbool = false;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.enabled = true;
            GameManager.Pool.Push(gameObject);
        }
    }
}
