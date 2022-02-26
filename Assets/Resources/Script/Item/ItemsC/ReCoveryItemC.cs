using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReCoveryItemC : ItemBase
{
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        DestroyClip = GetComponent<Animation>();
    }

    private void OnEnable()
    {
        rigid.velocity = new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f));
        DestroyClip.enabled = false;
    }

    private void OnDisable()
    {
        rigid.velocity = new Vector3(0, 0, 0);

    }

    private void Update()
    {
        ItemDestroy();
        if (_time > 7f && ClipBool == false)
        {
            DestroyClip.enabled = true;
            ClipBool = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, stageData.LimitMin.x, stageData.LimitMax.x),
        Mathf.Clamp(transform.position.y, stageData.LimitMin.y, stageData.LimitMax.y));
        if (other.gameObject.CompareTag("ItemLimit"))
        {
            Vector3 dir = transform.position - other.transform.position;        // RigidBody - Freeze Position 으로 z값 잠금
            rigid.AddForce((dir).normalized * 50f);
        }

        if (other.gameObject.CompareTag("Player"))
        {
            if (GameManager.Player.playerController.Stat.Hp < 100)       // 플레이어의 체력이 최대체력이 아닐시만 습득
            {
                GameManager.Sound.Play("Art/Sound/Effect/Item/ItemGet");
                GameManager.Player.playerController.Stat.Hp += scriptableObjectC.HpUp;
                GameManager.Pool.Push(gameObject);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, stageData.LimitMin.x, stageData.LimitMax.x),
        Mathf.Clamp(transform.position.y, stageData.LimitMin.y, stageData.LimitMax.y));

        if (other.gameObject.CompareTag("ItemLimit"))
        {
            rigid.velocity = new Vector3(0, 0);
            Vector3 dir = transform.position - other.transform.position;        // RigidBody - Freeze Position 으로 z값 잠금
            rigid.AddForce((dir).normalized * 50f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, stageData.LimitMin.x, stageData.LimitMax.x),
        Mathf.Clamp(transform.position.y, stageData.LimitMin.y, stageData.LimitMax.y));
    }
}
