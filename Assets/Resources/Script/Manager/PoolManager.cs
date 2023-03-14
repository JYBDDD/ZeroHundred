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
        var firstCheck = poolList.Where(_ => _.name.Contains(objectName));

        if (firstCheck.Count() <= 0)
            return null;

        foreach (var o in firstCheck)
        {
            if (o.activeSelf == false)
            {
                o.gameObject.SetActive(true);
                o.transform.SetPositionAndRotation(position, rotation);   // 코드 최적화
                return o;
            }
        }
        return null;

        /*
         * 바뀐것
        객체 풀링을 생성시 삽입정렬하도록 변경

        바꿀것
        삽입정렬된 객체를 이진탐색해서 찾을 것임 TODO


        문제사항

        1. 플레이어 스킬이 안나감
        2. Bullet이 가끔 안나오고 Null로 뻑날때 있음 (미사일 생길때인거같은데?)
         */
    }

    /* // 풀링 요소가 존재하는지 확인
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
