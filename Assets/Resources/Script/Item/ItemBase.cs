using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
