using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBackGroundPosition : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float scrollRange = 730f;
    [SerializeField]
    private float moveSpeed = 40f;
    [SerializeField]
    private Vector3 moveDirection = Vector3.down;

    private void Update()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        if (transform.position.x <= -scrollRange)
            transform.position = new Vector3(scrollRange,transform.position.y,transform.position.z);
    }
}
