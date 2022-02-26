using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMissileC : WeaponBase
{
    private float speed = 0.0f;             // �����̻��Ͽ� Movement2D ������� �ʰ� ��ü������ ������
    Vector3 lookVec = Vector3.zero;
    private float waitTime = 0.0f;

    AudioClip[] audioClips;         // �̻��� ���� ���� 3���� ���������� �����Ͽ� ������ �߻�

    private void Awake()
    {
        audioClips = Resources.LoadAll<AudioClip>("Art/Sound/Effect/Enemy/EnemyMissile/EnemyMissileHit");
    }

    private void OnEnable()
    {
        waitTime = 0;
        speed = 0;
    }

    private void FixedUpdate()
    {
        WeaponDestroyD();
        GuidedMissile();
    }

    private void GuidedMissile()        // ���� �̻���   
    {
        if (transform.position.z > 0.45f)        // �� �ڷ� �̵��� ������ ��� ����
        {
            GameManager.Pool.Push(gameObject);
            GameManager.Resource.Instantiate("Weapon/Missile/EnemyMissileHit", gameObject.transform.position, Quaternion.identity, GameManager.MuzzleOfHitParent.transform);
            if (Random.Range(0, 3) >= 2)      // �Ҹ��� �ʹ� ��ġ�� �ʵ��� �������� �Ҹ������� ���� �ʵ��� ����
                GameManager.Sound.Play("Art/Sound/Effect/Enemy/EnemyMissile/EnemyMissileExplosion");
            return;
        }

        waitTime += Time.deltaTime;
        if (waitTime < 0.7f) 
        {
            speed = Time.deltaTime;
            transform.Translate(-transform.forward * speed, Space.World);         // �ڷ� �̵�
        }
        else           
        {
            speed += Time.deltaTime / 40f;
            transform.Translate(transform.forward * speed, Space.World);

            lookVec = GameManager.Player.player.transform.position - transform.position;

            Quaternion qut = Quaternion.LookRotation(lookVec);          // �̻��� forward�� Ÿ���� ����
            transform.rotation = Quaternion.Slerp(transform.rotation, qut, Time.deltaTime * 6f);        // ������ ���̴� ���� Slerp�� ����
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            GameManager.Pool.Push(gameObject);
            GameManager.Resource.Instantiate("Weapon/Missile/EnemyMissileHit", gameObject.transform.position, Quaternion.Euler(-90, 0, 0), GameManager.MuzzleOfHitParent.transform);

            EnemyMissileC.MissileHitShake = true;

            GameManager.Sound.Play(audioClips[Random.Range(0, 3)]);
            other.GetComponent<PlayerControllerEx>().Stat.AttackDamage(other.GetComponent<PlayerControllerEx>().Stat, 4);       // BossMissile ������ ó��
        }
        if (other.transform.CompareTag("PlayerProjectile") | other.transform.CompareTag("Missile"))      // �����̻����� �÷��̾ �߻��ϴ� Projectile�� �������� �ʰ� ��� ������
        {
            GameManager.Pool.Push(gameObject);
            GameManager.Resource.Instantiate("Weapon/Missile/EnemyMissileHit", gameObject.transform.position, Quaternion.identity, GameManager.MuzzleOfHitParent.transform);

            if(Random.Range(0,3) >= 2)      // �Ҹ��� �ʹ� ��ġ�� �ʵ��� �������� �Ҹ������� ���� �ʵ��� ����
                GameManager.Sound.Play("Art/Sound/Effect/Enemy/EnemyMissile/EnemyMissileExplosion");
        }
    }
}
