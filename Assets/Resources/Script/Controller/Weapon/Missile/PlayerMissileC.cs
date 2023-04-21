using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissileC : MissileBase
{
    Vector3 lookVec = Vector3.zero;

    const string playerMisslieShotPath = "Art/Sound/Effect/Player/PlayerMissile/PlayerMissileShot";

    protected override void Inheritance()
    {
        base.Inheritance();
    }

    private void OnEnable()
    {
        StartCoroutine(StartMissile());
        Inheritance();
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void Update()
    {
        if (BaseController.ObjectList.Count > 0)
        {
            transform.forward = Vector3.Lerp(transform.forward, lookVec, 0.1f);
        }
    }

    IEnumerator StartMissile()
    {
        yield return null;

        bool isbool = true;

        while(isbool)
        {
            gameObject.GetComponent<Movement2D>().MoveDirection(new Vector3(0, -2f, 0));
            transform.rotation = Quaternion.Euler(-90, 0, 0);
            yield return new WaitForSeconds(0.5f);

            StartCoroutine(TrackingMissile());
            isbool = false;     // 0.5초후 사용 정지
        }
    }

    IEnumerator TrackingMissile()       // 목표 추적 (유도 아님)
    {
        yield return null;

        bool isbool = true;
        if(isbool)
        {
            GameManager.Sound.Play(playerMisslieShotPath);
            Movement2D m2D = gameObject.GetComponent<Movement2D>();

            if (BaseController.ObjectList.Count == 0)       // 적이 없을 시 전방을 향해 발사
            {
                m2D.MoveDirection(new Vector3(0, 10f, 0));
                transform.rotation = Quaternion.Euler(-90, 0, 0);
            }

            for (int i = 0; i < BaseController.ObjectList.Count; i++)
            {
                int randNum = Random.Range(0, BaseController.ObjectList.Count);     // 랜덤으로 적을 향해 발사
                lookVec = (BaseController.ObjectList[randNum].transform.position - transform.position).normalized;       // 적 위치 값

                m2D.MoveDirection(new Vector3(lookVec.x * 10f, lookVec.y * 10f));         // 이동 방향 결정
            }

        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    protected override void DamageProcess(Collider other)
    {
        PlayerMissileProcess(other);   
    }
}
