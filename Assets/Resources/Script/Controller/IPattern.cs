
public interface IEndGameObserver
{
    /// <summary>
    /// ���� ����� ȣ��
    /// </summary>
    public abstract void EndGame_Notice();
}

public interface IPattern
{
    public void Pattern();

    public void WeaponChange(Define.Weapon weapon);
}