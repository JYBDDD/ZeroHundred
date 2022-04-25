using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankToolSet : MonoBehaviour
{
    // 생성
    private void Awake()
    {
        for (int i = 0; i < 10; i++)
        {
            // Prefabs 랭크창 클론
            GameObject clone = GameManager.Resource.Instantiate("UI/Rank/1Rank", transform.position, new Quaternion(0,0,0,0),transform);

            // 순위
            Text text = clone.transform.GetChild(0).GetComponent<Text>();
            text.text = $"{i + 1}";

            // 순위 이미지
            Image img = clone.transform.GetChild(1).GetComponent<Image>();
            if(i+1 == 1)
            {
                Sprite[] spriteImg = Resources.LoadAll<Sprite>("Art/Textures/2D Casual UI/Sprite/GUI");
                img.sprite = spriteImg[4];
            }
            if (i+1 == 2)
            {
                Sprite[] spriteImg = Resources.LoadAll<Sprite>("Art/Textures/2D Casual UI/Sprite/GUI");
                img.sprite = spriteImg[5];
            }
            if (i+1 == 3)
            {
                Sprite[] spriteImg = Resources.LoadAll<Sprite>("Art/Textures/2D Casual UI/Sprite/GUI");
                img.sprite = spriteImg[6];
            }
            if(i+1 > 3)       // 순위권 밖일시 이미지를 출력시키지않음
            {
                img.enabled = false;
            }

            // 닉네임
            Text name = clone.transform.GetChild(3).transform.GetChild(0).GetComponent<Text>();
            name.text = $"{GameManager.BackendMain.GetRankListNickLookUp(i)}";

            // 점수
            Text score = clone.transform.GetChild(2).GetComponent<Text>();
            score.text = $"{GameManager.BackendMain.GetRankListScoreLookUp(i)}";
        }
    }

    // GetChild(1)의 이미지소스값을 1등일시 GUI_4 / 2등일시 GUI_5 / 3등일시 GUI_6 / 그외 image.endable = false;


}
