using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class ItemBase : MonoBehaviour
{
    [SerializeField]
    protected ScriptableObjectC scriptableObjectC;      // Scriptable 오브젝트에 들어있는 데이터 사용

    [SerializeField]
    protected StageData stageData;

    protected Rigidbody rigid;              // 생성시 폭발하듯 튕기도록 할것 (Item)

    protected Animation DestroyClip;        // 각각 자식 오브젝트의 깜빡이는 애니메이션 클립 사용 -> GetComponent 중
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
    /// Item 일정시간이 지난 후 삭제 (Update용) 깜빡이는 애니메이션 재생 용도 , 플레이어 체력이 0일시 즉시 삭제
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
            Vector3 dir = transform.position - other.transform.position;        // RigidBody - Freeze Position 으로 z값 잠금
            rigid.AddForce((dir).normalized * 100f);
        }
    }

    protected virtual void OnTriggerStay(Collider other)
    {
        transform.position = new Vector2(ClampX(), ClampY());
        if (other.gameObject.CompareTag("ItemLimit"))
        {
            rigid.velocity = new Vector3(0, 0);
            Vector3 dir = transform.position - other.transform.position;        // RigidBody - Freeze Position 으로 z값 잠금
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
