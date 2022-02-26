using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    // 최대 z값 : 4.66  최소 z값 -4.66
    private float time = 0;
    private Vector3 leftPos = Vector3.zero;
    private Vector3 rightPos = Vector3.zero;
    private Vector3 upPos = Vector3.zero;
    private Vector3 downPos = Vector3.zero;
    private int moveCount = 0;

    private void Start()
    {
        leftPos = new Vector3(transform.position.x, transform.position.y, transform.position.z - 4.66f);
        rightPos = new Vector3(transform.position.x, transform.position.y, transform.position.z + 4.66f);
        upPos = new Vector3(transform.position.x + 5f, transform.position.y, transform.position.z);
        downPos = new Vector3(transform.position.x - 14f, transform.position.y, transform.position.z);
        GameManager.Sound.Play("Art/Sound/BGM/StartScene_BGM", Define.Sound.bgm);
    }

    private void Update()
    {
        PlayerPosMove();
    }

    /// <summary>
    /// 플레이어 Pos값 변동(Update용)
    /// </summary>
    private void PlayerPosMove()
    {
        time += Time.deltaTime;

        if (time > 2f && moveCount == 0)
        {
            transform.position = Vector3.Lerp(transform.position, leftPos, Time.deltaTime / 2);

            if (time > 5f)
            {
                time = 0;
                moveCount = Random.Range(0, 4);
            }
        }

        if (time > 2f && moveCount == 1)
        {
            transform.position = Vector3.Lerp(transform.position, rightPos, Time.deltaTime / 2);

            if (time > 5f)
            {
                time = 0;
                moveCount = Random.Range(0, 4);
            }
        }

        if (time > 2f && moveCount == 2)
        {
            transform.position = Vector3.Lerp(transform.position, upPos, Time.deltaTime / 2);

            if (time > 5f)
            {
                time = 0;
                moveCount = Random.Range(0, 4);
            }
        }
        if (time > 2f && moveCount == 3)
        {
            transform.position = Vector3.Lerp(transform.position, downPos, Time.deltaTime / 2);

            if (time > 5f)
            {
                time = 0;
                moveCount = Random.Range(0, 4);
            }
        }
    }
}
