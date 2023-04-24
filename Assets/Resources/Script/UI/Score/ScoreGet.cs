using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;

public class ScoreGet : MonoBehaviour
{
    Text scoreText;

    float originScore;
    float middleScore;
    float targetScore;
    float offset;

    private void Awake()
    {
        GameManager.SCORE = 0;
        scoreText = GetComponent<Text>();
        originScore = GameManager.SCORE;
        this.UpdateAsObservable().Where(_ => GameManager.SCORE != originScore).Subscribe(_ => Counting());
    }

    private void Counting()                 // ������ �ö󰡴� ������ ����
    {
        targetScore = GameManager.SCORE;
        if (targetScore != middleScore)
        {
            float duration = 0.8f;        // ������ (�� �ƴ�)
            offset = (GameManager.SCORE - originScore) * duration;
            middleScore = targetScore;
        }

        originScore += offset * Time.deltaTime;
        scoreText.text = ((int)originScore).ToString();

        if(originScore > GameManager.SCORE)
        {
            originScore = GameManager.SCORE;
            scoreText.text = ((int)originScore).ToString();
        }

    }
}
