using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Controller : BaseController
{
    [SerializeField]
    private GameObject machineGun;
    [SerializeField]
    private GameObject missileLauncher;

    public static Stat BossStatStatic;

    // StartMove �����
    float startTime = 0;
    int startCount = 0;

    // BossPattern
    float time;         // ���� ���� Ÿ��
    bool fanShotSoundBool = false;

    // FlashBang ����
    public static bool FlashBangBool = false;

    protected override void OnEnable()
    {
        base.OnEnable();

        //stat = GameManager.Json.LoadJsonFile<Stat>(Application.dataPath + $"/Data", "Boss1Stat");      // ���� �ҷ�����   (PC ����)
        stat = GameManager.Json.AndroidLoadJson<Stat>("Data/Boss1Stat");       // ���� (Android ��)


        time = 10;

        machineGun.SetActive(false);
        missileLauncher.SetActive(false);

        BossStatStatic = stat;
    }

    private void Start()
    {
        //StatSet(true);        // ���� ����
    }

    private void Update()
    {
        if(GameManager.Player.playerController.Stat.Hp <= 0)        // �÷��̾� ����� ����
            GameManager.Pool.Push(gameObject);

        StartMove();
        StateHit("BossSwitchColor1");

        if(FlashBangBool == false && stat.Hp == 800 | stat.Hp == 500 | stat.Hp == 300)        // ü���� ������ ���ٽ�
        {
            GameManager.Sound.Play("Art/Sound/Effect/Enemy/Boss/FlashBang");
            GameManager.Resource.Instantiate("Weapon/FlashBang/FlashB",transform.position,Quaternion.identity,GameManager.EnemyBulletParent.transform);
            FlashBangBool = true;
        }
    }

    private void StartMove()
    {
        startTime += Time.deltaTime;
        if (startTime < 1.9f)           // Boss1 ũ�� ����
        {
            gameObject.transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0.05f, 0.05f, 0.05f), Time.deltaTime);
        }
        #region Boss1 ù ������ �ӵ� ����
        if (startTime >= 1.5f && startCount == 0)
        {
            startCount++;
            gameObject.GetComponent<Movement2D>().MoveDirection(new Vector3(0, 0.8f));
        }
        if (startTime >= 1.7f && startCount == 1)
        {
            startCount++;
            gameObject.GetComponent<Movement2D>().MoveDirection(new Vector3(0, 0.6f));
        }
        if (startTime >= 1.9f && startCount == 2)
        {
            startCount++;
            gameObject.GetComponent<Movement2D>().MoveDirection(new Vector3(0, 0.4f));
        }
        if (startTime >= 2.1f && startCount == 3)
        {
            startCount++;
            gameObject.GetComponent<Movement2D>().MoveDirection(new Vector3(0, 0.2f));
        }
        if (startTime >= 2.3f && startCount == 4)
        {
            startCount++;
            gameObject.GetComponent<Movement2D>().MoveDirection(new Vector3(0, 0));
            StartCoroutine(BossPatternCor());
        }
        #endregion
    }

    IEnumerator BossPatternCor()
    {
        while (true)
        {
            time += Time.deltaTime;

            if (time > 10)
            {
                time = 0;
                switch (Random.Range(0, 5))           // ������ ���������� ����  (0,5)
                {
                    case 0:
                        MachineGunFiring();
                        break;
                    case 1:
                        MissileLaunch();
                        break;
                    case 2:
                        AroundFire();
                        break;
                    case 3:
                        FanShot();
                        break;
                    case 4:
                        AirMine();
                        break;
                }
            }
        }
    }

    /// <summary>
    /// ���� ���� ����
    /// </summary>
    public void BossPartternStop()
    {
        StopCoroutine(BossPatternCor());
        GameManager.Sound.Play("Art/Sound/BGM/GameScene_BGM");      // ���� óġ�� �ٽ� ���Ӿ�_BGM �ε�
        machineGun.SetActive(false);
        missileLauncher.SetActive(false);
    }

    private void MachineGunFiring()     // �÷��̾� ���� ���
    {
        fanShotSoundBool = false;
        machineGun.SetActive(true);
        missileLauncher.SetActive(false);
;
    }

    private void MissileLaunch()        // ���� �̻��� �߻�
    {
        fanShotSoundBool = false;
        missileLauncher.SetActive(true);
        machineGun.SetActive(false);
    }

    private void AroundFire()           // ���� ������� Projectile �߻�
    {
        fanShotSoundBool = false;
        missileLauncher.SetActive(false);
        machineGun.SetActive(false);

        int aroundFireA = 50;
        int aroundFireB = 43;
        int aroundFireC = 32;
        int aroundFire1 = Random.Range(0,2) % 2 == 0 ? aroundFireA : aroundFireB;
        int aroundFireNum = Random.Range(0,2) % 2 == 0 ? aroundFire1 : aroundFireC;

        for(int i = 0; i < aroundFireA; i++)
        {
            if(stat.Hp > 0 && gameObject.activeSelf)
            {
                GameObject bullet = GameManager.Resource.Instantiate("Weapon/Bullet/EnemyProjectile", transform.position, Quaternion.identity, GameManager.EnemyBulletParent.transform);
                Rigidbody rigid = bullet.GetComponent<Rigidbody>();

                Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 2 * i / aroundFireNum)
                                            , Mathf.Sin(Mathf.PI * 2 * i / aroundFireNum));
                rigid.AddForce(dirVec.normalized * 3f, ForceMode.Impulse);
            }
        }

        if (time < 7f && stat.Hp > 0 && gameObject.activeSelf)
        {
            Invoke("AroundFire", 1f);
            GameManager.Sound.Play("Art/Sound/Effect/Enemy/Boss/BossAroundShot");
        }
    }

    private void FanShot()              // ��ä�� ������� Roket �߻�
    {
        missileLauncher.SetActive(false);
        machineGun.SetActive(false);

        if(stat.Hp > 0 && gameObject.activeSelf)
        {
            GameObject bullet = GameManager.Resource.Instantiate("Weapon/Rocket/Rocket Projectile", transform.position, Quaternion.identity, GameManager.EnemyBulletParent.transform);
            Rigidbody rigid = bullet.GetComponent<Rigidbody>();
            Vector2 dirvec = new Vector2(Random.Range(-0.5f, 0.5f), -1);
            rigid.AddForce(dirvec.normalized * 5f, ForceMode.Impulse);
        }

        if(time < 8.5f && stat.Hp > 0 && gameObject.activeSelf)
        {
            Invoke("FanShot", 0.1f);
        }
        FanShotSound();
    }

    private void AirMine()              // ���� ���� ������ ����
    {
        missileLauncher.SetActive(false);
        machineGun.SetActive(false);

        if(stat.Hp > 0 && gameObject.activeSelf)
        {
            Vector2 dirvec = new Vector2(Random.Range(-2.6f, 2.6f), Random.Range(-4.6f, 2.5f));
            GameObject bullet = GameManager.Resource.Instantiate("Weapon/Bombing/Bombing", dirvec, Quaternion.identity, GameManager.EnemyBulletParent.transform);
        }
        if (time < 8.5f && stat.Hp > 0 && gameObject.activeSelf)
            Invoke("AirMine", 0.25f);
    }


    public void BossDead()
    {
        StartCoroutine(BossDeadCor());

        IEnumerator BossDeadCor()
        {
            float time = 0;
            float deadEffectCall = 0;
            Vector2 dirvec = new Vector2(Random.Range(-2f, 2f), Random.Range(1.5f, 3f));        // DeadEffect ���� ��ġ��

            while (true)
            {
                time += Time.deltaTime;
                deadEffectCall += Time.deltaTime;

                if(deadEffectCall > 0.25f)
                {
                    GameManager.Resource.Instantiate("Boss/BossDeadEffect", dirvec, Quaternion.identity, GameManager.DeadEffectParent.transform);       // 0.25�ʸ��� ȣ��ǵ��� ����
                    deadEffectCall = 0;
                }

                if(time > 3f)
                {
                    GameManager.Sound.Play("Art/Sound/Effect/Enemy/Boss/BossDie");
                    GameManager.Resource.Instantiate("Boss/Boss1FinalDeadEffect", transform.position, Quaternion.identity, GameManager.DeadEffectParent.transform);
                    GameManager.Pool.Push(gameObject);
                    yield break;
                }

                yield return null;
            }
        }
    }

    private void FanShotSound()     // FanShot����
    {
        if(fanShotSoundBool == false)
        {
            GameManager.Sound.Play("Art/Sound/Effect/Enemy/Boss/BossRoketShot");
            fanShotSoundBool = true;
        }
    }
}
