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

    // StartMove 사용중
    float startTime = 0;
    int startCount = 0;

    // BossPattern
    float time;         // 패턴 변경 타임
    bool fanShotSoundBool = false;

    // FlashBang 설정
    public static bool FlashBangBool = false;

    protected override void OnEnable()
    {
        base.OnEnable();

        //stat = GameManager.Json.LoadJsonFile<Stat>(Application.dataPath + $"/Data", "Boss1Stat");      // 스탯 불러오기   (PC 전용)
        stat = GameManager.Json.AndroidLoadJson<Stat>("Data/Boss1Stat");       // 스탯 (Android 용)


        time = 10;

        machineGun.SetActive(false);
        missileLauncher.SetActive(false);

        BossStatStatic = stat;
    }

    private void Start()
    {
        //StatSet(true);        // 스탯 생성
    }

    private void Update()
    {
        if(GameManager.Player.playerController.Stat.Hp <= 0)        // 플레이어 사망시 삭제
            GameManager.Pool.Push(gameObject);

        StartMove();
        StateHit("BossSwitchColor1");

        if(FlashBangBool == false && stat.Hp == 800 | stat.Hp == 500 | stat.Hp == 300)        // 체력이 일정량 접근시
        {
            GameManager.Sound.Play("Art/Sound/Effect/Enemy/Boss/FlashBang");
            GameManager.Resource.Instantiate("Weapon/FlashBang/FlashB",transform.position,Quaternion.identity,GameManager.EnemyBulletParent.transform);
            FlashBangBool = true;
        }
    }

    private void StartMove()
    {
        startTime += Time.deltaTime;
        if (startTime < 1.9f)           // Boss1 크기 조정
        {
            gameObject.transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0.05f, 0.05f, 0.05f), Time.deltaTime);
        }
        #region Boss1 첫 생성시 속도 조절
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
                switch (Random.Range(0, 5))           // 패턴을 랜덤값으로 결정  (0,5)
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
    /// 보스 패턴 종료
    /// </summary>
    public void BossPartternStop()
    {
        StopCoroutine(BossPatternCor());
        GameManager.Sound.Play("Art/Sound/BGM/GameScene_BGM");      // 보스 처치후 다시 게임씬_BGM 로드
        machineGun.SetActive(false);
        missileLauncher.SetActive(false);
    }

    private void MachineGunFiring()     // 플레이어 조준 사격
    {
        fanShotSoundBool = false;
        machineGun.SetActive(true);
        missileLauncher.SetActive(false);
;
    }

    private void MissileLaunch()        // 유도 미사일 발사
    {
        fanShotSoundBool = false;
        missileLauncher.SetActive(true);
        machineGun.SetActive(false);
    }

    private void AroundFire()           // 원형 모양으로 Projectile 발사
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

    private void FanShot()              // 부채꼴 모양으로 Roket 발사
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

    private void AirMine()              // 일정 범위 무작위 폭격
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
            Vector2 dirvec = new Vector2(Random.Range(-2f, 2f), Random.Range(1.5f, 3f));        // DeadEffect 랜덤 위치값

            while (true)
            {
                time += Time.deltaTime;
                deadEffectCall += Time.deltaTime;

                if(deadEffectCall > 0.25f)
                {
                    GameManager.Resource.Instantiate("Boss/BossDeadEffect", dirvec, Quaternion.identity, GameManager.DeadEffectParent.transform);       // 0.25초마다 호출되도록 설정
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

    private void FanShotSound()     // FanShot사운드
    {
        if(fanShotSoundBool == false)
        {
            GameManager.Sound.Play("Art/Sound/Effect/Enemy/Boss/BossRoketShot");
            fanShotSoundBool = true;
        }
    }
}
