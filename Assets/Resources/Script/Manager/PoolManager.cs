using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PoolManager : IManager
{
    public static List<GameObject> poolList = new List<GameObject>();

    public void Push(GameObject pushObject)  // �����Ǵ°����� ����ȣ��
    {
        pushObject.SetActive(false);
    }

    public GameObject Pop(string objectName,Vector2 position,Quaternion rotation)
    {
        var firstCheck = poolList.Where(_ => _.name.Contains(objectName));

        if (firstCheck.Count() <= 0)
            return null;

        foreach (var o in firstCheck)
        {
            if (o.activeSelf == false)
            {
                o.gameObject.SetActive(true);
                o.transform.SetPositionAndRotation(position, rotation);   // �ڵ� ����ȭ
                return o;
            }
        }
        return null;

        /*
         * �ٲ��
        ��ü Ǯ���� ������ ���������ϵ��� ����

        �ٲܰ�
        �������ĵ� ��ü�� ����Ž���ؼ� ã�� ���� TODO


        ��������

        1. �÷��̾� ��ų�� �ȳ���
        2. Bullet�� ���� �ȳ����� Null�� ������ ���� (�̻��� ���涧�ΰŰ�����?)
         */
    }

    /* // Ǯ�� ��Ұ� �����ϴ��� Ȯ��
     public bool PoolingExists(string name,out GameObject outObj)
     {
         outObj = null;
         var firstCheck = poolList.Where(_ => _.name.Contains(name));

         if (firstCheck.Count() <= 0)
             return false;

         foreach(var o in firstCheck)
         {
             if (o.activeSelf == false)
             {
                 outObj = o;
                 return true;
             }
         }

         return false;
     }*/

    // ��� ���� ����
    public void InsertMerge(GameObject obj)
    {
        poolList.Insert(0, obj);
        //poolList.OrderBy(_ => _.name);

        for (int i = 1; i < poolList.Count; ++i)
        {
            int iMinuse = i - 1;

            // �����ų ������ Ŭ��� ����
            if (GameManager.Resource.CompareLowCode(poolList[iMinuse].name, poolList[i].name) == true)
                break;
            else
            {
                GameObject temp = poolList[i];
                poolList[i] = poolList[iMinuse];
                poolList[iMinuse] = temp;
            }
        }
    }

    public void Init()
    {
        
    }

    public void OnUpdate()
    {
        
    }

    public void Clear()
    {
        poolList.Clear();
    }
}
