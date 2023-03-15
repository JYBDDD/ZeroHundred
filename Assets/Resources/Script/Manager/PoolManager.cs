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
        IEnumerable<GameObject> result = from useList in poolList
                             where useList.activeSelf == false
                             where useList.name.Contains(objectName)
                             select useList;

        var useObj = result.ToList().First();               // 여기서 에러 떠서 찾는중... TODO
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
         * 바뀐것
        객체 풀링을 생성시 삽입정렬하도록 변경
        이후 IEnumerable 인터페이스를 사용하여 조건에 맞는 첫번째 값 조인


        문제사항

        3. 최대체력 이상으로 맞은것으로 판단되는 객체가 회전값 0,0,0으로 맞춰지는 버그 (이거 죽자마자 이진탐색되서 )
         */
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
