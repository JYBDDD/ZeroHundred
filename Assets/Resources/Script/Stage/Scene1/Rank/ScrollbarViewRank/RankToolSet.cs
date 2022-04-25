using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankToolSet : MonoBehaviour
{
    // ����
    private void Awake()
    {
        for (int i = 0; i < 10; i++)
        {
            // Prefabs ��ũâ Ŭ��
            GameObject clone = GameManager.Resource.Instantiate("UI/Rank/1Rank", transform.position, new Quaternion(0,0,0,0),transform);

            // ����
            Text text = clone.transform.GetChild(0).GetComponent<Text>();
            text.text = $"{i + 1}";

            // ���� �̹���
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
            if(i+1 > 3)       // ������ ���Ͻ� �̹����� ��½�Ű������
            {
                img.enabled = false;
            }

            // �г���
            Text name = clone.transform.GetChild(3).transform.GetChild(0).GetComponent<Text>();
            name.text = $"{GameManager.BackendMain.GetRankListNickLookUp(i)}";

            // ����
            Text score = clone.transform.GetChild(2).GetComponent<Text>();
            score.text = $"{GameManager.BackendMain.GetRankListScoreLookUp(i)}";
        }
    }

    // GetChild(1)�� �̹����ҽ����� 1���Ͻ� GUI_4 / 2���Ͻ� GUI_5 / 3���Ͻ� GUI_6 / �׿� image.endable = false;


}
