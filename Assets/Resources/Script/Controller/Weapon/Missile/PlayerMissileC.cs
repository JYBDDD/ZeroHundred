using System.Collections;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class PlayerMissileC : MissileBase
{
    Vector3 lookVec = Vector3.zero;

    const string playerMisslieShotPath = "Art/Sound/Effect/Player/PlayerMissile/PlayerMissileShot";

    private void Start()
    {
        this.UpdateAsObservable().Where(_ => BaseController.ObjectList.Count > 0).Subscribe(_ => LookAtMissile());
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

    private void LookAtMissile()
    {
        transform.forward = Vector3.Lerp(transform.forward, lookVec, 0.1f);
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
            GameManager.Sound.Play(playerMisslieShotPath);
            Movement2D m2D = gameObject.GetComponent<Movement2D>();

            if (BaseController.ObjectList.Count == 0)       // ���� ���� �� ������ ���� �߻�
            {
                m2D.MoveDirection(new Vector3(0, 10f, 0));
                transform.rotation = Quaternion.Euler(-90, 0, 0);
            }

            int randNum = Random.Range(0, BaseController.ObjectList.Count);     // �������� ���� ���� �߻�
            lookVec = (BaseController.ObjectList[randNum].transform.position - transform.position).normalized;       // �� ��ġ ��

            m2D.MoveDirection(new Vector3(lookVec.x * 10f, lookVec.y * 10f));         // �̵� ���� ����

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
