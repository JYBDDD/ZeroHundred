using Path;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class BossSpawn : MonoBehaviour
{
    [SerializeField]
    GameObject enemyPhase;
    Boss1Controller boss;

    float time = 0;     //  ���� ����� 5�ʵ� ����ǵ��� ����

    private void OnEnable()
    {
        GameObject bossObj = GameManager.Resource.Instantiate("Boss/Boss1", transform.position, Quaternion.Euler(-90, 0, 0), GameManager.EnemyObjectParent.transform);
        boss = bossObj.GetComponent<Boss1Controller>();
        GameManager.Sound.Play(SceneSound_P.BossBGM, Define.Sound.bgm);

        this.LateUpdateAsObservable().Where(_ => boss.Stat.hp <= 0).Subscribe(_ => BossSpawnerOff());
    }

    private void BossSpawnerOff()
    {
        time += Time.deltaTime;
        if (time > 5f)
        {
            gameObject.SetActive(false);
            enemyPhase.SetActive(true);
        }
    }
}
