using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy5Controller : BaseController
{
    [SerializeField]
    private StageData stageData;

    protected override void OnEnable()
    {
        base.OnEnable();

        //stat = GameManager.Json.LoadJsonFile<Stat>(Application.dataPath + $"/Data", "Enemy5Stat");    // ½ºÅÈ ºÒ·¯¿À±â  (PC Àü¿ë)
        stat = GameManager.Json.AndroidLoadJson<Stat>("Data/Enemy5Stat");       // ½ºÅÈ

        ObjectList.Add(gameObject);
        StartCoroutine(BombWarning());

        GameManager.Sound.Play("Art/Sound/Effect/Enemy/Enemy5Move/Enemy5MoveSound");
    }

    private void OnDisable()
    {
        ObjectList.Remove(gameObject);
        StopCoroutine(BombWarning());
    }

    private void Start()
    {
        //StatSet(true);        // ½ºÅÈ »ý¼º
    }

    private void FixedUpdate()
    {
        DestroyObject();
        StateHit("SwitchColor2");
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
