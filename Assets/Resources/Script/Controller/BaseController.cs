using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    // ���� ������ -> Enemy,Player
    protected new Animation animation;      // �ڽ��� Start�� OnEnable ���� GetComponent ȣ���������
    public bool animBool = false;

    protected Stat stat = new Stat();
    public Stat Stat { get => stat; }

    public static List<GameObject> ObjectList = new List<GameObject>();

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
    /// ������Ʈ�� ü���� 0�� �Ǿ����� ���� ����Ʈ�� ����(Update��)
    /// </summary>
    protected void ObjectDead()
    {
        if (stat.hp <= 0)
        {
            GameManager.Pool.Push(gameObject);
            GameManager.Resource.Instantiate("Public/DeadEffect", transform.position, Quaternion.identity, GameManager.DeadEffectParent.transform);
            GameManager.Sound.Play("Art/Sound/Effect/Enemy/EnemyDie/EnemyDie");
            GameManager.SCORE += stat.DropScore;        // ���� ȹ��
            GameManager.Resource.ItemInstantiate(transform.position, Quaternion.identity, GameManager.ItemObjectParent.transform);  // ���ο��� 10% Ȯ���� ���� ������ ����
        }
    }

    #region �� Ÿ�ݽ� �Ͻ��� ���� ����
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
