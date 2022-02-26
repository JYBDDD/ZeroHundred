using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    private void Awake()
    {
        if (GameManager.Player.player != null)
            return;

        GameManager.Player.player = GameManager.Resource.Instantiate("PlayerP/Player",transform.position, Quaternion.Euler(-180,0,0));
        GameManager.Player.playerController = GameManager.Player.player.GetComponent<PlayerControllerEx>();
    }

}
