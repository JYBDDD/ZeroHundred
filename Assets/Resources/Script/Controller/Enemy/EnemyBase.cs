using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : BaseController
{
    protected float lookVec = 0.0f;
    protected Quaternion lookQut = Quaternion.identity;

    /// <summary>
    /// �︮���� �迭 �� �̵� ����
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
            yield return new WaitForSeconds(randRange);                                  // ���� �ð� ���
            gameObject.GetComponent<Movement2D>().MoveDirection(new Vector3(0, 0, 0));     // �̵� ���� 

            yield return new WaitForSeconds(2f);                                         // 2���� ��� ����
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
                    lookVec = gameObject.transform.position.x - GameManager.Player.player.transform.position.x;   // �÷��̾� ��ġ�� ���� �ٸ� ��
                    lookQut = (lookVec >= 0) ? Quaternion.Euler(145, -20, 180) : Quaternion.Euler(145, 20, 180);
                    check = false;
                }

                transform.rotation = Quaternion.Slerp(transform.rotation, lookQut, Time.deltaTime * 2);
            }

            yield return null;
        }
    }
}
