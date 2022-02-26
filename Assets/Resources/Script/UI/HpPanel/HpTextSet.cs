using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpTextSet : MonoBehaviour
{
    Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    private void FixedUpdate()
    {
        TextColor();
    }

    private void TextColor()
    {
        text.text = $"{GameManager.Player.playerController.Stat.Hp}";

        if (GameManager.Player.playerController.Stat.Hp > 66)
            text.color = Color.blue;
        if (GameManager.Player.playerController.Stat.Hp > 36 && GameManager.Player.playerController.Stat.Hp <= 66)
            text.color = Color.yellow;
        if (GameManager.Player.playerController.Stat.Hp <= 36)
            text.color = Color.red;
    }
}
