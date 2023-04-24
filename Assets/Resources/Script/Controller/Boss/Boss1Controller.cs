using Path;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class Boss1Controller : EnemyBase,IEndGameObserver
{
    [SerializeField]
    private GameObject machineGun;
    [SerializeField]
    private GameObject missileLauncher;
    [SerializeField]
    private int specialHpSub;

    // StartMove �����
    float startTime = 0;
    int startCount = 0;

    // BossPattern
    float time;         // ���� ���� Ÿ��
    bool fanShotSoundBool = false;

    // FlashBang ����
    public static bool FlashBangBool = false;

    // -- UniRx ���� ���� ���� ����
    IDisposable startLogic;

    protected override void Awake()
    {
        GameManager.Instance.EndGame_AddObserver(this);
        this.UpdateAsObservable().Where(_ => FlashBangBool == false && stat.Hp == specialHpSub).Subscribe(_ => SpecialPattern());
        startLogic = this.UpdateAsObservable().Where(_ => startTime < 3f).Subscribe(_ => StartMove());
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        SpreadData();


        time = 10;

        machineGun.SetActive(false);
        missileLauncher.SetActive(false);
    }

    private void StartMove()
    {
        startTime += Time.deltaTime;
        if (startTime < 1.9f)           // Boss1 ũ�� ����
        {
            gameObject.transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0.05f, 0.05f, 0.05f), Time.deltaTime);
        }
        #region Boss1 ù ������ �ӵ� ����
        BossSpeedAdjust(1.5f, 0, 0.8f);
        BossSpeedAdjust(1.7f, 1, 0.6f);
        BossSpeedAdjust(1.9f, 2, 0.4f);
        BossSpeedAdjust(2.1f, 3, 0.2f);
        BossSpeedAdjust(2.3f, 4, 0,()=> { StartCoroutine(BossPatternCor()); startLogic.Dispose(); });
        #endregion
    }

    private void BossSpeedAdjust(float checkTime, int checkCount, float speed, Action addAction = null)
    {
        if (startTime >= checkTime && startCount == checkCount)
        {
            startCount++;
            gameObject.GetComponent<Movement2D>().MoveDirection(new Vector3(0, speed));
            
            if(addAction != null)
            {
                addAction.Invoke();
            }
        }
    }

    IEnumerator BossPatternCor()
    {
        while (true)
        {
            time += Time.deltaTime;

            if (time > 10)
            {
                time = 0;
                switch (UnityEngine.Random.Range(0, 5))           // ������ ���������� ����  (0,5)
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

            yield return null;
        }
    }

    /// <summary>
    /// ���� ���� ����
    /// </summary>
    public void BossPartternStop()
    {
        StopCoroutine(BossPatternCor());
        GameManager.Sound.Play(SceneSound_P.GameSceneBGM);      // ���� óġ�� �ٽ� ���Ӿ�_BGM �ε�
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
        int aroundFire1 = UnityEngine.Random.Range(0,2) % 2 == 0 ? aroundFireA : aroundFireB;
        int aroundFireNum = UnityEngine.Random.Range(0,2) % 2 == 0 ? aroundFire1 : aroundFireC;

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
            GameManager.Sound.Play(ObjSound_P.BossAroundShot);
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
            Vector2 dirvec = new Vector2(UnityEngine.Random.Range(-0.5f, 0.5f), -1);
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
            Vector2 dirvec = new Vector2(UnityEngine.Random.Range(-2.6f, 2.6f), UnityEngine.Random.Range(-4.6f, 2.5f));
            GameManager.Resource.Instantiate("Weapon/Bombing/Bombing", dirvec, Quaternion.identity, GameManager.EnemyBulletParent.transform);
        }
        if (time < 8.5f && stat.Hp > 0 && gameObject.activeSelf)
            Invoke("AirMine", 0.25f);
    }

    // Ư�� ����
    private void SpecialPattern()
    {
        GameManager.Sound.Play(ObjSound_P.FlashBang);
        GameManager.Resource.Instantiate("Weapon/FlashBang/FlashB", transform.position, Quaternion.identity, GameManager.EnemyBulletParent.transform);
        FlashBangBool = true;
    }

    public void BossDead()
    {
        StartCoroutine(BossDeadCor());

        IEnumerator BossDeadCor()
        {
            float time = 0;
            float deadEffectCall = 0;
            Vector2 dirvec = new Vector2(UnityEngine.Random.Range(-2f, 2f), UnityEngine.Random.Range(1.5f, 3f));        // DeadEffect ���� ��ġ��

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
                    GameManager.Sound.Play(ObjSound_P.BossDie);
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
            GameManager.Sound.Play(ObjSound_P.BossRocketShot);
            fanShotSoundBool = true;
        }
    }

    public void EndGame_Notice()
    {
        GameManager.Pool.Push(gameObject);
    }
}
