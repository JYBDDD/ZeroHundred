using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    // ���� ������ -> Enemy,Player
    protected new Animation animation;      // �ڽ��� Start�� OnEnable ���� GetComponent ȣ���������
    public bool animBool = false;

    protected Stat stat = new Stat();
    public Stat Stat { get => stat; }

    private Define.ObjType _objType = Define.ObjType.None;
    public Define.ObjType ObjType { get => _objType; }

    public static List<GameObject> ObjectList = new List<GameObject>();

    protected virtual void OnEnable()
    {
        animation = GetComponent<Animation>();
        _objType = EnemyReturnState();
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
            Debug.Log("���� ����");
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
    /// ���� ����Ʈ�� ����(Update��) - Enemy�� �ش�
    /// </summary>
    public void EnemyDead()
    {
        GameManager.Pool.Push(gameObject);
        GameManager.Resource.Instantiate("Public/DeadEffect", transform.position, Quaternion.identity, GameManager.DeadEffectParent.transform);
        GameManager.Sound.Play("Art/Sound/Effect/Enemy/EnemyDie/EnemyDie");
        GameManager.SCORE += stat.DropScore;        // ���� ȹ��
        GameManager.Resource.ItemInstantiate(transform.position, Quaternion.identity, GameManager.ItemObjectParent.transform);  // ���ο��� 10% Ȯ���� ���� ������ ����
    }

    #region �� Ÿ�ݽ� �Ͻ��� ���� ����
    protected void StateHit(string animationName)
    {
        if (animBool == true)
        {
            // �ǰ� �ִϸ��̼ǰ� ȥ��
            animation.Blend(animationName,10,0.03f);

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
