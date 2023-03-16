using Path;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    // 공통 데이터 -> Enemy,Player
    protected new Animation animation;
    public bool animBool = false;

    protected string id = string.Empty;
    [SerializeField]
    protected Stat stat = new Stat();
    public Stat Stat { get => stat; }

    private Define.ObjType _objType = Define.ObjType.None;
    public Define.ObjType ObjType { get => _objType; }

    public static List<GameObject> ObjectList = new List<GameObject>();

    protected virtual void Awake() {}

    protected virtual void OnEnable()
    {
        animation = GetComponent<Animation>();
        _objType = EnemyReturnState();
    }

    /// <summary>
    /// 데이터 삽입
    /// </summary>
    protected void SpreadData()
    {
        if (id == string.Empty)
        {
            if (this.name.Contains("Clone"))
                id = this.name.Split('(')[0];
            else
                id = this.name.Split('/')[1];
        }

        stat = GameManager.Json.AndroidLoadJson<Stat>($"Data/{id}Stat");       // 스탯
    }

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
    /// 적 사망
    /// </summary>
    public void EnemyDead()
    {
        GameManager.Pool.Push(gameObject);
        GameManager.Resource.Instantiate("Public/DeadEffect", transform.position, Quaternion.identity, GameManager.DeadEffectParent.transform);
        GameManager.Sound.Play(ObjSound_P.EnemyDie);
        GameManager.SCORE += stat.DropScore;        // 점수 획득
        GameManager.Resource.ItemInstantiate(transform.position, Quaternion.identity, GameManager.ItemObjectParent.transform);  // 내부에서 10% 확률로 랜덤 아이템 생성
    }

    #region 적 타격시 일시적 색상 변경
    public void StateHit_Enemy(string objName)
    {
        if (animBool == true)
        {
            StringBuilder sb = new StringBuilder();
            if (objName.Contains("Boss"))
                sb.Append("BossSwitchColor1");
            else
            {
                int num = 2;
                if (objName.Contains("1_"))
                    num = 1;
                sb.Append("SwitchColor" + num);
            }


            // 피격 애니메이션과 혼합
            animation.Blend(sb.ToString(), 1, 0.01f);

            animBool = false;
            Invoke("EnemyReturnState", 0.5f);
        }
    }

    protected Define.ObjType EnemyReturnState()
    {
        string objN = this.name;

        if (objN.Contains("Player"))
            return Define.ObjType.Player;
        else if (objN.Contains("Enemy"))
        {
            // 적 필요 인덱스 반환
            int idx = objN.IndexOf("Enemy") + 5;

            if(int.TryParse(objN[idx].ToString(), out int num))
            {
                if (num >= 2)
                    animation.Play("Enemy2");
                else
                    animation.Play("Enemy1");
            }
            animation.Rewind();
            return Define.ObjType.Enemy;
        }
        else if(objN.Contains("Boss"))
        {
            animation.Play("Boss1");
            return Define.ObjType.Boss;
        }
        else
            return Define.ObjType.None;

    }
    #endregion
}
