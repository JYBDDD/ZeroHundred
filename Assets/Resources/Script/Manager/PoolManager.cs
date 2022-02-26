using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : IManager
{
    public static List<GameObject> poolList = new List<GameObject>();

    public void Push(GameObject pushObject)  // 삭제되는곳에서 직접호출
    {
        pushObject.SetActive(false);
        poolList.Add(pushObject);
    }

    public GameObject Pop(string objectname,Vector2 position,Quaternion rotation)
    {
        for (int i = 0; i < poolList.Count; i++)
        {
            if (objectname == poolList[i].gameObject.name && !poolList[i].activeSelf)
            {
                poolList[i].transform.position = position;
                poolList[i].transform.rotation = rotation;
                poolList[i].gameObject.SetActive(true);
                return poolList[i].gameObject;
            }
        }
        return null;

    }

    public void Init()
    {
        
    }

    public void OnUpdate()
    {
        
    }

    public void Clear()
    {
        
    }
}
