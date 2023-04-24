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

    private void Counting()                 // 점수가 올라가는 딜레이 설정
    {
        targetScore = GameManager.SCORE;
        if (targetScore != middleScore)
        {
            float duration = 0.8f;        // 딜레이 (초 아님)
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
