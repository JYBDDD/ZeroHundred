using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    public T Load<T>(string path) where T : UnityEngine.Object
    {
        T Path = Resources.Load<T>(path);

        if (Path == null)
            return null;

        return Path;
    }

    public GameObject Instantiate(string path,Vector2 position,Quaternion rotation,Transform parent = null)
    {
        GameObject go = Load<GameObject>($"Prefabs/{path}");

        if (go == null)
            return null;

        if (parent == GameManager.PlayerBulletParent.transform)                             // Player Weapon 犁积己
        {
            for (int i = 0; i < GameManager.PlayerBulletParent.transform.childCount; i++)
            {
                if (GameManager.PlayerBulletParent.transform.GetChild(i).name.Contains(path) && !GameManager.PlayerBulletParent.transform.GetChild(i).gameObject.activeSelf)
                {
                    return GameManager.Pool.Pop(path, position, rotation);
                }
            }
        }

        if (parent == GameManager.EnemyBulletParent.transform)                              // Enemy Weapon 犁积己
        {
            for (int i = 0; i < GameManager.EnemyBulletParent.transform.childCount; i++)
            {
                if (GameManager.EnemyBulletParent.transform.GetChild(i).name.Contains(path) && !GameManager.EnemyBulletParent.transform.GetChild(i).gameObject.activeSelf)
                {
                    return GameManager.Pool.Pop(path, position, rotation);
                }
            }
        }

        if (parent == GameManager.MuzzleOfHitParent.transform)                              // Muzzle,HitEffect 犁积己
        {
            for (int i = 0; i < GameManager.MuzzleOfHitParent.transform.childCount; i++)
            {
                if (GameManager.MuzzleOfHitParent.transform.GetChild(i).name.Contains(path) && !GameManager.MuzzleOfHitParent.transform.GetChild(i).gameObject.activeSelf)
                {
                    return GameManager.Pool.Pop(path, position, rotation);
                }
            }
        }

        if (parent == GameManager.EnemyObjectParent.transform)                              // Enemy Object 犁积己
        {
            for (int i = 0; i < GameManager.EnemyObjectParent.transform.childCount; i++)
            {
                if (GameManager.EnemyObjectParent.transform.GetChild(i).name.Contains(path) && !GameManager.EnemyObjectParent.transform.GetChild(i).gameObject.activeSelf)
                {
                    return GameManager.Pool.Pop(path, position, rotation);
                }
            }
        }

        if (parent == GameManager.DeadEffectParent.transform)                               // DeadEffect 犁积己
        {
            for (int i = 0; i < GameManager.DeadEffectParent.transform.childCount; i++)
            {
                if (GameManager.DeadEffectParent.transform.GetChild(i).name.Contains(path) && !GameManager.DeadEffectParent.transform.GetChild(i).gameObject.activeSelf)
                {
                    return GameManager.Pool.Pop(path, position, rotation);
                }
            }
        }


        GameObject clone = UnityEngine.Object.Instantiate(go, position, rotation, parent);
        clone.name = path;

        return clone;
    }

    private GameObject ItemsPrefabs()     // 酒捞袍甸狼 坷宏璃飘甫 历厘
    {
        GameObject[] gameObjects = Resources.LoadAll<GameObject>("Prefabs/Item");

        int randNum = Random.Range(0, gameObjects.Length);

        return gameObjects[randNum];        // 酒捞袍甸吝 窍唱 公累困 眠免
    }

    public GameObject ItemInstantiate(Vector2 position, Quaternion rotation, Transform parent = null)
    {
        if(Random.Range(0,100) < 10)        // 10% 犬伏肺 积己
        {
            if (parent == GameManager.ItemObjectParent.transform)                               // ItemObject 犁积己
            {
                for (int i = 0; i < GameManager.ItemObjectParent.transform.childCount; i++)
                {
                    if (GameManager.ItemObjectParent.transform.GetChild(i).name.Contains(ItemsPrefabs().name) && !GameManager.ItemObjectParent.transform.GetChild(i).gameObject.activeSelf)
                    {
                        return GameManager.Pool.Pop(ItemsPrefabs().name, position, rotation);
                    }
                }
            }

            GameObject clone = UnityEngine.Object.Instantiate(ItemsPrefabs(), position, rotation, parent);
            clone.name = ItemsPrefabs().name;

            return clone;
        }
        return null;
    }

    // 匙飘况农


}
