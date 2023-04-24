using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class ItemBase : MonoBehaviour
{
    [SerializeField]
    protected ScriptableObjectC scriptableObjectC;      // Scriptable ������Ʈ�� ����ִ� ������ ���

    [SerializeField]
    protected StageData stageData;

    protected Rigidbody rigid;              // ������ �����ϵ� ƨ�⵵�� �Ұ� (Item)

    protected Animation DestroyClip;        // ���� �ڽ� ������Ʈ�� �����̴� �ִϸ��̼� Ŭ�� ��� -> GetComponent ��
    protected bool ClipBool = false;

    protected float _time = 0;


    protected  void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        DestroyClip = GetComponent<Animation>();
        this.UpdateAsObservable().Subscribe(_ => DestroyCheck());
    }

    protected void OnEnable()
    {
        rigid.velocity = new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f));
        DestroyClip.enabled = false;
    }

    protected void OnDisable()
    {
        rigid.velocity = new Vector3(0, 0, 0);
    }

    private void DestroyCheck()
    {
        ItemDestroy();
        if (_time > 7f && ClipBool == false)
        {
            DestroyClip.enabled = true;
            ClipBool = true;
        }
    }

    /// <summary>
    /// Item �����ð��� ���� �� ���� (Update��) �����̴� �ִϸ��̼� ��� �뵵 , �÷��̾� ü���� 0�Ͻ� ��� ����
    /// </summary>
    protected void ItemDestroy()
    {
        _time += Time.deltaTime;

        if(_time > 10f | GameManager.Player.playerController.Stat.Hp <= 0)
        {
            _time = 0;
            ClipBool = false;
            GameManager.Pool.Push(gameObject);
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        transform.position = new Vector2(ClampX(),ClampY());
        if (other.gameObject.CompareTag("ItemLimit"))
        {
            Vector3 dir = transform.position - other.transform.position;        // RigidBody - Freeze Position ���� z�� ���
            rigid.AddForce((dir).normalized * 100f);
        }
    }

    protected virtual void OnTriggerStay(Collider other)
    {
        transform.position = new Vector2(ClampX(), ClampY());
        if (other.gameObject.CompareTag("ItemLimit"))
        {
            rigid.velocity = new Vector3(0, 0);
            Vector3 dir = transform.position - other.transform.position;        // RigidBody - Freeze Position ���� z�� ���
            rigid.AddForce((dir).normalized * 50f);
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        transform.position = new Vector2(ClampX(), ClampY());
    }

    private float ClampX()
    {
        return Mathf.Clamp(transform.position.x, stageData.LimitMin.x, stageData.LimitMax.x);
    }

    private float ClampY()
    {
        return Mathf.Clamp(transform.position.y, stageData.LimitMin.y, stageData.LimitMax.y);
    }
}
