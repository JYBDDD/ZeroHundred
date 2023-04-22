using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class Movement2D : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 0.0f;
    [SerializeField]
    private Vector3 moveDirection = Vector3.zero;

    private void Awake()
    {
        this.UpdateAsObservable().Subscribe(_ => MoveDirectUpdate());
    }

    private void MoveDirectUpdate()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    public void MoveDirection(Vector3 dircection) // 외부 이동 조절
    {
        moveDirection = dircection;
    }
}
