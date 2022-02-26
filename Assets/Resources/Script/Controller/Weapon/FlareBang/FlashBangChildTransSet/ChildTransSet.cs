using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildTransSet : MonoBehaviour
{
    Vector3 myPos;      // 오브젝트 위치를 MoveMent2D로 변경시키고있으므로 재생성시 위치값 기본값으로 재변경

    private void Awake()
    {
        myPos = transform.position;
    }

    private void OnEnable()
    {
        transform.position = myPos;
    }
}
