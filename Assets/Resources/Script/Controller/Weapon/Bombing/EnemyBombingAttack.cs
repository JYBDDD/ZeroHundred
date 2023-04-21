using Path;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class EnemyBombingAttack : WeaponBase
{
    private bool isbool;
    private float time = 0;

    private void Awake()
    {
        this.UpdateAsObservable().Where(_ => time < 0.11f).Subscribe(_=> HitCheck());
    }

    protected override void Initialize()
    {
        isbool = true;
        time = 0;
        GameManager.Sound.Play(ObjSound_P.BombingExplosion);
    }

    private void OnEnable()
    {
        Initialize();
    }

    private void HitCheck()
    {
        if (isbool == true)
            time += Time.deltaTime;

        if (time > 0.1f)         // OnEnable 되었을때 0.1초까지만 타격 판정
            isbool = false;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
    }

    protected override void DamageProcess(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && isbool)
        {
            // 삭제 시켜주는 구문은 따로 넣어놓음
            PlayerControllerEx pEx = other.GetComponent<PlayerControllerEx>();
            pEx.Stat.AttackDamage(pEx.Stat, 30);       // Bombing 데미지 처리
        }
    }
}
