using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawn : MonoBehaviour
{
    [SerializeField]
    GameObject enemyPhase;

    float time = 0;     //  ���� ����� 5�ʵ� ����ǵ��� ����

    private void OnEnable()
    {
        GameManager.Resource.Instantiate("Boss/Boss1", transform.position, Quaternion.Euler(-90, 0, 0), GameManager.EnemyObjectParent.transform);
        GameManager.Sound.Play("Art/Sound/BGM/Boss_BGM", Define.Sound.bgm);
    }

    private void Update()
    {
        if (Boss1Controller.BossStatStatic.hp <= 0)
        {
            time += Time.deltaTime;
            if(time > 5f)
            {
                gameObject.SetActive(false);
                enemyPhase.SetActive(true);
            }
        }
    }
}
