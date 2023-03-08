using System.Collections;
using UnityEngine;

public class Enemy5Controller : EnemyBase
{
    [SerializeField]
    private StageData stageData;

    protected override void OnEnable()
    {
        base.OnEnable();

        SpreadData();

        ObjectList.Add(gameObject);
        StartCoroutine(BombWarning());

        GameManager.Sound.Play("Art/Sound/Effect/Enemy/Enemy5Move/Enemy5MoveSound");
    }

    private void OnDisable()
    {
        ObjectList.Remove(gameObject);
        StopCoroutine(BombWarning());
    }

    private void FixedUpdate()
    {
        DestroyObject();
    }

    IEnumerator BombWarning()
    {
        yield return null;

        bool isbool = true;

        if(isbool)
        {
            yield return new WaitForSeconds(1f);

            Vector3 randVec = new Vector3(Random.Range(stageData.LimitMin.x, stageData.LimitMax.x), Random.Range(stageData.LimitMin.y, stageData.LimitMax.y));
            GameManager.Resource.Instantiate("Weapon/Bombing/Bombing", randVec, Quaternion.identity, GameManager.EnemyBulletParent.transform);
            isbool = false;
        }
    }
}
