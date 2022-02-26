using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreGet : MonoBehaviour
{
    Text scoreText;

    float originScore;

    private void Awake()
    {
        GameManager.SCORE = 0;
        scoreText = GetComponent<Text>();
        originScore = GameManager.SCORE;
    }

    private void Update()
    {
        if(GameManager.SCORE != originScore)            // ���� �ٸ����� ȣ��
            StartCoroutine(Count());
    }

    IEnumerator Count()                 // ������ �ö󰡴� ������ ����
    {
        float duration = 20f;        // ������ (�� �ƴ�)
        float offset = (GameManager.SCORE - originScore) / duration;

        while (originScore < GameManager.SCORE)
        {
            originScore += offset * Time.deltaTime;
            scoreText.text = ((int)originScore).ToString();
            yield return null;
        }

        originScore = GameManager.SCORE;
        scoreText.text = ((int)originScore).ToString();
    }
}
