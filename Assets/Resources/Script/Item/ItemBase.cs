using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
