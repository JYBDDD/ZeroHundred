using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextZero : MonoBehaviour
{
    float time = 0;
    Text text;
    bool isBool = false;

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    private void Start()
    {
        text.enabled = false;
    }

    private void OnEnable()
    {
        time = 0;
        isBool = false;
    }

    private void Update()
    {
        time += Time.deltaTime;

        if (time > 0.1f && isBool == false)
        {
            text.enabled = true;
            isBool = true;
        }
    }
}
