using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using UnityEngine.UI;

public class HashCodeField : MonoBehaviour
{
    [SerializeField]
    InputField input;

    public void GetGoogleThisHashColde()
    {
        GameManager.BackendMain.GetGoogleHashCode(input);
    }
}
