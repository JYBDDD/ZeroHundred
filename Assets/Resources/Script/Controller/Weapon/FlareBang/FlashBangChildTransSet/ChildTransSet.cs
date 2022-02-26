using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildTransSet : MonoBehaviour
{
    Vector3 myPos;      // ������Ʈ ��ġ�� MoveMent2D�� �����Ű�������Ƿ� ������� ��ġ�� �⺻������ �纯��

    private void Awake()
    {
        myPos = transform.position;
    }

    private void OnEnable()
    {
        transform.position = myPos;
    }
}
