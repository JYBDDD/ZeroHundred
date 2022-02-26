using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissileC : WeaponBase
{
    public static bool MissileHitShake = false;

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
        GameManager.Sound.Play("Art/Sound/Effect/Enemy/EnemyMissile/EnemyMissileShot");
    }

    private void FixedUpdate()
    {
        WeaponDestroyD();
        GuidedMissile();
    }

    private void GuidedMissile()        // ���� �̻���   
    {
        if(transform.position.z > 0.45f)        // �� �ڷ� �̵��� ������ ��� ����
        {
            if (Random.Range(0, 3) >= 2)      // �Ҹ��� �ʹ� ��ġ�� �ʵ��� �������� �Ҹ������� ���� �ʵ��� ����
                GameManager.Sound.Play("Art/Sound/Effect/Enemy/EnemyMissile/EnemyMissileExplosion");
            GameManager.Pool.Push(gameObject);
            GameManager.Resource.Instantiate("Weapon/Missile/EnemyMissileHit", gameObject.transform.position, Quaternion.identity, GameManager.MuzzleOfHitParent.transform);
            return;
        }

        waitTime += Time.deltaTime;
        if(waitTime < 1.5f)     // 1.5�ʰ� �̻��� ���� �ӷ� ����
        {
            speed = Time.deltaTime;
            transform.Translate(transform.forward * speed, Space.World);         // forward �������� �̵�
        }
        else                    // 1.5���� �ӷ� ��� ����
        {
            speed += Time.deltaTime / 10f;
            transform.Translate(transform.forward * speed, Space.World);       
        }

        lookVec = GameManager.Player.player.transform.position - transform.position;

        Quaternion qut = Quaternion.LookRotation(lookVec);          // �̻��� forward�� Ÿ���� ����
        transform.rotation = Quaternion.Slerp(transform.rotation, qut, Time.deltaTime * 2f);        // ������ ���̴� ���� Slerp�� ����
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Player"))
        {
            GameManager.Pool.Push(gameObject);
            GameManager.Resource.Instantiate("Weapon/Missile/EnemyMissileHit", gameObject.transform.position, Quaternion.Euler(-90,0,0), GameManager.MuzzleOfHitParent.transform);

            MissileHitShake = true;

            GameManager.Sound.Play(audioClips[Random.Range(0,3)]);
            other.GetComponent<PlayerControllerEx>().Stat.AttackDamage(other.GetComponent<PlayerControllerEx>().Stat, 4);       // EnemyMissile ������ ó��
        }
        if(other.transform.CompareTag("PlayerProjectile") | other.transform.CompareTag("Missile"))      // �����̻����� �÷��̾ �߻��ϴ� Projectile�� �������� �ʰ� ��� ������
        {
            GameManager.Pool.Push(gameObject);
            GameManager.Resource.Instantiate("Weapon/Missile/EnemyMissileHit", gameObject.transform.position, Quaternion.identity, GameManager.MuzzleOfHitParent.transform);

            if (Random.Range(0, 3) >= 2)      // �Ҹ��� �ʹ� ��ġ�� �ʵ��� �������� �Ҹ������� ���� �ʵ��� ����
                GameManager.Sound.Play("Art/Sound/Effect/Enemy/EnemyMissile/EnemyMissileExplosion");
        }
    }

}
