using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissileC : WeaponBase
{
    Vector3 lookVec = Vector3.zero;

    private void OnEnable()
    {
        StartCoroutine(StartMissile());
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
        WeaponDestroyD();
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
            isbool = false;     // 0.5���� ��� ����
        }
    }

    IEnumerator TrackingMissile()       // ��ǥ ���� (���� �ƴ�)
    {
        yield return null;

        bool isbool = true;
        if(isbool)
        {
            GameManager.Sound.Play("Art/Sound/Effect/Player/PlayerMissile/PlayerMissileShot");

            if (BaseController.ObjectList.Count == 0)       // ���� ���� �� ������ ���� �߻�
            {
                gameObject.GetComponent<Movement2D>().MoveDirection(new Vector3(0, 10f, 0));
                transform.rotation = Quaternion.Euler(-90, 0, 0);
            }

            for (int i = 0; i < BaseController.ObjectList.Count; i++)
            {
                int randNum = Random.Range(0, BaseController.ObjectList.Count);     // �������� ���� ���� �߻�
                lookVec = (BaseController.ObjectList[randNum].transform.position - gameObject.transform.position).normalized;       // �� ��ġ ��

                gameObject.GetComponent<Movement2D>().MoveDirection(new Vector3(lookVec.x * 10f, lookVec.y * 10f));         // �̵� ���� ����
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            GameManager.Pool.Push(gameObject);
            GameManager.Resource.Instantiate("Weapon/Missile/MissileHit", transform.position, Quaternion.identity, GameManager.MuzzleOfHitParent.transform);

            GameManager.Sound.Play("Art/Sound/Effect/Player/PlayerMissile/PlayerMissileHit");
            other.gameObject.GetComponent<BaseController>().Stat.AttackDamage(other.gameObject.GetComponent<BaseController>().Stat, 2);     // Missile ������ ó��
            other.gameObject.GetComponent<BaseController>().animBool = true;
        }
        if (other.gameObject.CompareTag("EnemyWeaponD"))        // �ı������� ������Ʈ�� ������ ����
        {
            GameManager.Pool.Push(gameObject);
            GameManager.Resource.Instantiate("Weapon/Missile/MissileHit", gameObject.transform.position, Quaternion.identity, GameManager.MuzzleOfHitParent.transform);
        }
    }
}