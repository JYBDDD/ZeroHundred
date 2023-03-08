using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : BaseController
{
    protected float lookVec = 0.0f;
    protected Quaternion lookQut = Quaternion.identity;

    /// <summary>
    /// 헬리콥터 계열 적 이동 지정
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    protected IEnumerator MoveRoutin_Enemy1(int min,int max)
    {
        yield return null;

        bool isbool = true;
        while (isbool)
        {
            float randRange = Random.Range(min, max);
            yield return new WaitForSeconds(randRange);                                  // 일정 시간 대기
            gameObject.GetComponent<Movement2D>().MoveDirection(new Vector3(0, 0, 0));     // 이동 정지 

            yield return new WaitForSeconds(2f);                                         // 2초후 사용 정지
            isbool = false;
        }
    }

    protected IEnumerator MoveDirect_Enemy1()
    {
        float _time = 1.5f;
        float time = 0;
        bool check = true;
        lookQut = Quaternion.identity;

        while (true)
        {
            time += Time.deltaTime;

            if (time > 3f)
                yield break;

            if (time > _time)
            {
                if (check == true)
                {
                    lookVec = gameObject.transform.position.x - GameManager.Player.player.transform.position.x;   // 플레이어 위치에 따라 다른 값
                    lookQut = (lookVec >= 0) ? Quaternion.Euler(145, -20, 180) : Quaternion.Euler(145, 20, 180);
                    check = false;
                }

                transform.rotation = Quaternion.Slerp(transform.rotation, lookQut, Time.deltaTime * 2);
            }

            yield return null;
        }
    }
}
