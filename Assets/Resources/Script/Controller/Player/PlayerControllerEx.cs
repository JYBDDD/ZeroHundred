using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerEx : BaseController
{
    private int playerSkillCount = 1;       // 첫 시작시 기본 스킬 카운트를 1개로 고정
    public int PlayerSkillCount 
    {
        get => playerSkillCount; 
        set
        {
            playerSkillCount = Mathf.Clamp(playerSkillCount, 0, 3);     // 스킬의 최대 저장량을 3개로 한정
            playerSkillCount = value;
        }
    }

    [SerializeField]
    private StageData stageData;

    [SerializeField]
    private float moveSpeed = 3.0f;

    public Animator _anim;

    #region        스킬 실행시 사용되는 것
    [SerializeField]
    private GameObject[] gameObjects;           // 플레이어 무기 저장
    private bool NotTumble = false;
    private float TumbleTime = 0;               // 3초후 임시 사격정지 해제
    #endregion

    protected override void Awake()
    {
        SpreadData();
    }

    protected override void OnEnable()
    {
        _anim = GetComponent<Animator>();
        base.OnEnable();
    }

    public void Movement(Vector2 inputDirection)
    {
        bool isMove = inputDirection.magnitude != 0;

        if (isMove)
        {
            Vector3 upMovement = Vector3.up * inputDirection.y * moveSpeed * Time.deltaTime;
            Vector3 rightMovement = Vector3.right * inputDirection.x * moveSpeed * Time.deltaTime;

            transform.position += upMovement + rightMovement;

            if (rightMovement.x < 0)
            {
                _anim.SetBool("LeftS", true);
                _anim.SetBool("RightS", false);
            }
            if (rightMovement.x > 0)
            {
                _anim.SetBool("RightS", true);
                _anim.SetBool("LeftS", false);
            }

            // 화면밖으로 넘어가지 않도록 설정
            transform.position = new Vector2(Mathf.Clamp(transform.position.x, stageData.LimitMin.x, stageData.LimitMax.x),
                Mathf.Clamp(transform.position.y, stageData.LimitMin.y, stageData.LimitMax.y));
        }
        if (!isMove)
        {
            _anim.SetBool("LeftS", false);
            _anim.SetBool("RightS", false);
        }

    }

    private void LateUpdate()  
    {
        AnimTumbleSet();
    }

    private void AnimTumbleSet()
    {
        if (_anim.GetBool("TumbleB"))           
        {
            TumbleTime += Time.deltaTime;

            if (TumbleTime > 3f)
            {
                _anim.SetBool("TumbleB",false);
                TumbleTime = 0;
            }

            for (int i = 0; i < gameObjects.Length; i++)
            {
                if (gameObjects[i].activeSelf)
                {
                    gameObjects[i].SetActive(false);
                    NotTumble = true;
                }
            }
        }

        if (!_anim.GetBool("TumbleB") && NotTumble == true)      
        {
            NotTumble = false;
            for (int i = 0; i < gameObjects.Length; i++)
            {
                gameObjects[i].SetActive(true);
            }
        }
    }

    /// <summary>
    /// 플레이어 사망
    /// </summary>
    public void PlayerDead()
    {
        GameManager.Sound.Play("Art/Sound/Effect/Player/PlayerDie");
        // 폭발 이펙트
        GameManager.Resource.Instantiate("Public/DeadEffect", transform.position, Quaternion.identity, GameManager.DeadEffectParent.transform);
        gameObject.SetActive(false);
    }
}
