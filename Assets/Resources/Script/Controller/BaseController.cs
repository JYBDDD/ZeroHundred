using Path;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    // ���� ������ -> Enemy,Player
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
    /// ������ ����
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

        stat = GameManager.Json.AndroidLoadJson<Stat>($"Data/{id}Stat");       // ����
    }

    /// <summary>
    /// �ʱ� ���� ����
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
    /// ī�޶󿡼� ������ġ �̻� �־����� �� ������Ʈ ���� or �÷��̾ �׾����� ��� ����
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
    /// �� ���
    /// </summary>
    public void EnemyDead()
    {
        GameManager.Pool.Push(gameObject);
        GameManager.Resource.Instantiate("Public/DeadEffect", transform.position, Quaternion.identity, GameManager.DeadEffectParent.transform);
        GameManager.Sound.Play(ObjSound_P.EnemyDie);
        GameManager.SCORE += stat.DropScore;        // ���� ȹ��
        GameManager.Resource.ItemInstantiate(transform.position, Quaternion.identity, GameManager.ItemObjectParent.transform);  // ���ο��� 10% Ȯ���� ���� ������ ����
    }

    #region �� Ÿ�ݽ� �Ͻ��� ���� ����
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


            // �ǰ� �ִϸ��̼ǰ� ȥ��
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
            // �� �ʿ� �ε��� ��ȯ
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
