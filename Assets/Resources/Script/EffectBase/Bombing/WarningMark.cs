using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningMark : MonoBehaviour        // EnemyBombingC 와 폭발 타이밍 맞춰놓음
{
    Vector3 originMark;
    Vector3 minSizeMark;
    Vector3 minusValue;
    Vector3 plusValue;

    bool isbool;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        originMark = new Vector3(0.13f, 0.13f, 0.13f);
        plusValue = new Vector3(0.1283f, 0.1283f, 0.1283f);

        minSizeMark = new Vector3(0.07f, 0.07f, 0.07f);
        minusValue = new Vector3(0.078f, 0.078f, 0.078f);
    }

    private void OnEnable()
    {
        spriteRenderer.enabled = true;
        isbool = true;
        transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        
    }

    private void Update()
    {
        if(!isbool)
            MarkGrowUpSize();
        MarkShrinkSize();
    }

    private void MarkShrinkSize()       // Mark 사이즈 줄이기
    {
        if(isbool)
        {
            transform.localScale = Vector3.Slerp(transform.localScale, minSizeMark, Time.deltaTime * 10f);

            if(transform.localScale.magnitude <= minusValue.magnitude)
                isbool = false;
        }
    }

    private void MarkGrowUpSize()       // Mark 사이즈 늘이기
    {
        transform.localScale = Vector3.Slerp(transform.localScale, originMark, Time.deltaTime * 5f);
        if (transform.localScale.magnitude >= plusValue.magnitude)
            spriteRenderer.enabled = false;
    }
}
