
public class Enemy3Controller : EnemyBase
{
    protected override void OnEnable()
    {
        base.OnEnable();

        SpreadData();

        ObjectList.Add(gameObject);
    }

    private void OnDisable()
    {
        ObjectList.Remove(gameObject);
    }

    private void FixedUpdate()
    {
        DestroyObject();
    }
}
