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
        IEnumerable<GameObject> result = from useList in poolList
                             where useList.activeSelf == false
                             where useList.name.Contains(objectName)
                             select useList;

        var useObj = result.ToList().First();               // ���⼭ ���� ���� ã����... TODO
        return useObj;

        /*var firstCheck = poolList.Where(_ => _.name.Contains(objectName));

        if (firstCheck.Count() <= 0)
            return null;

        foreach (var o in firstCheck)
        {
            if (o.activeSelf == false)
            {
                o.transform.SetPositionAndRotation(position, rotation);
                o.gameObject.SetActive(true);
                return o;
            }
        }
        return null;*/

        /*
         * �ٲ��
        ��ü Ǯ���� ������ ���������ϵ��� ����
        ���� IEnumerable �������̽��� ����Ͽ� ���ǿ� �´� ù��° �� ����


        ��������

        3. �ִ�ü�� �̻����� ���������� �ǴܵǴ� ��ü�� ȸ���� 0,0,0���� �������� ���� (�̰� ���ڸ��� ����Ž���Ǽ� )
         */
    }

    // ��� ���� ����
    public void InsertMerge(GameObject obj)
    {
        poolList.Insert(0, obj);
        //poolList.OrderBy(_ => _.name);

        for (int i = 1; i < poolList.Count; ++i)
        {
            int iMinuse = i - 1;

            // �����ų ������ Ŭ��� ����
            if (GameManager.Resource.CompareLowCode(poolList[iMinuse].name, poolList[i].name,out char c) == true)
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
