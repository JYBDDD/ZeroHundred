using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillButton : MonoBehaviour
{
    public void SkillButtonClick()
    {
        if(GameManager.Player.playerController.PlayerSkillCount > 0)
        {
            GameManager.Player.playerController.PlayerSkillCount--;
            GameManager.Player.playerController._anim.SetBool("TumbleB",true);

            GameManager.Resource.Instantiate("PlayerP/TotalPlane", new Vector3(2, -6, 0), Quaternion.identity, GameManager.PlayerBulletParent.transform);
        }
    }
}
