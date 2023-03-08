using UnityEngine;

public class Enemy1_1Controller : EnemyBase
{
    protected override void OnEnable()
    {
        base.OnEnable();

        SpreadData();

        ObjectList.Add(gameObject);
        StartCoroutine(MoveRoutin_Enemy1(5,10));
        StartCoroutine(MoveDirect_Enemy1());
    }
    private void OnDisable()
    {
        gameObject.GetComponent<Movement2D>().MoveDirection(new Vector3(0, -1, 0));
        ObjectList.Remove(gameObject);
        StopAllCoroutines();
    }
    private void FixedUpdate()
    {
        DestroyObject();
    }
}
