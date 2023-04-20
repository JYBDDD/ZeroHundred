
public interface IEndGameObserver
{
    /// <summary>
    /// 게임 종료시 호출
    /// </summary>
    public abstract void EndGame_Notice();
}

public interface IPattern
{
    public void Pattern();

    public void WeaponChange(Define.Weapon weapon);
}