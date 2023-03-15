using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PoolManager : IManager
{
    public static List<GameObject> poolList = new List<GameObject>();

    public void Push(GameObject pushObject)  // 삭제되는곳에서 직접호출
    {
        pushObject.SetActive(false);
    }

    public GameObject Pop(string objectName,Vector2 position,Quaternion rotation)
    {
        // Linq 쿼리 Where 사용하여 특정 조건값에 만족하는 객체 발견시 검색종료 및 반환

        var firstCheck = poolList.Where(_ => _.name.Contains(objectName));

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
        return null;
    }

    // 요소 삽입 정렬
    public void InsertMerge(GameObject obj)
    {
        poolList.Insert(0, obj);
        //poolList.OrderBy(_ => _.name);

        for (int i = 1; i < poolList.Count; ++i)
        {
            int iMinuse = i - 1;

            // 변경시킬 값보다 클경우 종료
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
