using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    // 공통 데이터 -> Enemy,Player
    protected new Animation animation;      // 자식의 Start나 OnEnable 에서 GetComponent 호출해줘야함
    public bool animBool = false;

    protected Stat stat = new Stat();
    public Stat Stat { get => stat; }

    public static List<GameObject> ObjectList = new List<GameObject>();

    /// <summary>
    /// 초기 스탯 설정
    /// </summary>
    /// <param name="isSet"></param>
    public void StatSet(bool isSet)
    {
        if (isSet)
        {
            stat.hp = 1000;
            stat.maxHp = 1000;
            stat.dropScore = 100;

            string jsonData = GameManager.Json.ObjectToJson(stat);
            GameManager.Json.CreateJsonFile(Application.dataPath, "Boss1Stat", jsonData);
            Debug.Log("스탯 생성");
        }
    }

    /// /// <summary>
    /// 카메라에서 일정위치 이상 멀어졌을 시 오브젝트 삭제 or 플레이어가 죽었을시 즉시 삭제
    /// </summary>
    /// <param name="isSet"></param>
    protected void DestroyObject()
    {
        float cameraDistance = (Camera.main.transform.position - transform.position).magnitude;

        if (cameraDistance >= 13f | GameManager.Player.playerController.stat.Hp <= 0)
        {
            GameManager.Pool.Push(gameObject);
        }
    }

    /// <summary>
    /// 오브젝트의 체력이 0이 되었을때 폭발 이펙트후 삭제(Update용)
    /// </summary>
    protected void ObjectDead()
    {
        if (stat.hp <= 0)
        {
            GameManager.Pool.Push(gameObject);
            GameManager.Resource.Instantiate("Public/DeadEffect", transform.position, Quaternion.identity, GameManager.DeadEffectParent.transform);
            GameManager.Sound.Play("Art/Sound/Effect/Enemy/EnemyDie/EnemyDie");
            GameManager.SCORE += stat.DropScore;        // 점수 획득
            GameManager.Resource.ItemInstantiate(transform.position, Quaternion.identity, GameManager.ItemObjectParent.transform);  // 내부에서 10% 확률로 랜덤 아이템 생성
        }
    }

    #region 적 타격시 일시적 색상 변경
    protected void StateHit(string animationName,string returnMethodName)
    {
        if (animBool == true)
        {
            animation.Play(animationName);
            animBool = false;
            Invoke(returnMethodName, 0.1f);
        }

    }

    protected void Enemy1ReturnState()
    {
        animation.Play("Enemy1");
    }

    protected void Enemy2ReturnState()
    {
        animation.Play("Enemy2");
    }

    protected void Boss1ReturnState()
    {
        animation.Play("Boss1");
    }
    #endregion

}
