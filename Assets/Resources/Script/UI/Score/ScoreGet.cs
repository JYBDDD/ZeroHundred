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
        if(GameManager.SCORE != originScore)            // 값이 다를때만 호출
            StartCoroutine(Count());
    }

    IEnumerator Count()                 // 점수가 올라가는 딜레이 설정
    {
        float duration = 20f;        // 딜레이 (초 아님)
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
