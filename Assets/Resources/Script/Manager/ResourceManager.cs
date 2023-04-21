using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class ResourceManager
{
    GameObject[] itemObjects;

    public T Load<T>(string path) where T : UnityEngine.Object
    {
        T Path = Resources.Load<T>(path);

        if (Path == null)
            return null;

        return Path;
    }

    /// <summary>
    /// ����
    /// </summary>
    /// <param name="path"></param>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    public GameObject Instantiate(string path,Vector2 position,Quaternion rotation,Transform parent = null)
    {
        PoolManager pool = GameManager.Pool;
        if (parent != null)
        {
            StringBuilder sb = new StringBuilder();
            string[] nameArr = path.Split('/');
            sb.Append(nameArr[nameArr.Length - 1] + "(Clone)");

            GameObject popItem = pool.Pop(sb.ToString(), position, rotation);
            if (popItem != null)
            {
                return popItem;
            }
        }

        // Ǯ���� ��ü�� ������� �ε� �� ����
        GameObject go = Load<GameObject>($"Prefabs/{path}");

        if (go == null)
            return null;

        GameObject clone = UnityEngine.Object.Instantiate(go, position, rotation, parent);
        pool.InsertMerge(clone);        // ���� ����

        return clone;
    }

    private GameObject ItemsPrefabs()     // �����۵��� ������Ʈ�� ����
    {
        itemObjects ??= Resources.LoadAll<GameObject>("Prefabs/Item");

        int randNum = UnityEngine.Random.Range(0, itemObjects.Length);

        return itemObjects[randNum];        // �����۵��� �ϳ� ������ ����
    }

    public GameObject ItemInstantiate(Vector2 position, Quaternion rotation, Transform parent = null)
    {
        PoolManager pool = GameManager.Pool;

        if (UnityEngine.Random.Range(0,100) < 10)        // 10% Ȯ���� ����
        {
            GameObject dropItem = ItemsPrefabs();
            if (parent == GameManager.ItemObjectParent.transform)                               // ItemObject �����
            {
                GameObject popItem = pool.Pop(dropItem.name, position, rotation);
                if (popItem != null)
                    return popItem;
            }

            GameObject clone = UnityEngine.Object.Instantiate(dropItem, position, rotation, parent);
            pool.InsertMerge(clone);        // ���� ����

            return clone;
        }
        return null;
    }

    public void DestroyObject_UniRx<T>(T o, GameObject obj) where T : MonoBehaviour
    {
        o.UpdateAsObservable().Where(_ => (Camera.main.transform.position - obj.transform.position).magnitude >= 13f ||
        GameManager.Player.playerController.Stat.Hp <= 0).Subscribe(_ => GameManager.Pool.Push(obj)).AddTo(o);
    }

    /// <summary>
    /// ���ڿ� �ƽ�Ű�ڵ尪�� ���Ͽ� compareA���� �۴ٸ� true
    /// </summary>
    /// <returns></returns>
    public bool CompareLowCode(string compareA,string compareB)
    {
        byte[] cA = Encoding.ASCII.GetBytes(compareA);
        byte[] cB = Encoding.ASCII.GetBytes(compareB);

        for(int i = 0; i < cA.Length; ++i)
        {
            if (i > cB.Length)
                return false;

            var codeA = int.Parse(cA[i].ToString());
            var codeB = int.Parse(cB[i].ToString());

            if (codeA > codeB)
                return false;
            else if (codeB > codeA)
                return true;
        }

        return false;
    }
}
