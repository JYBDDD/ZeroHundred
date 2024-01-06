using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    public static GameManager Instance 
    {
        get 
        { 
            Init(); 
            return instance; 
        } 
    }

    PlayerManager _player = new PlayerManager();
    ResourceManager _resource = new ResourceManager();
    PoolManager _pool = new PoolManager();
    JsonManager _json = new JsonManager();
    SoundManager _sound = new SoundManager();
    NetworkMain _network = new NetworkMain();

    public static PlayerManager Player { get => Instance._player; }
    public static ResourceManager Resource { get => Instance._resource; }
    public static PoolManager Pool { get => Instance._pool; }
    public static JsonManager Json { get => Instance._json; }
    public static SoundManager Sound { get => Instance._sound; }
    public static NetworkMain NetworkMain { get => Instance._network; }

    // Player
    public static GameObject PlayerBulletParent;    // Player Weapon �θ�
    // Enemy
    public static GameObject EnemyObjectParent;     // Enemy Object �θ�
    public static GameObject EnemyBulletParent;     // Enemy Weapon �θ�
    // Item
    public static GameObject ItemObjectParent;      // Itme Object �θ�
    // ����
    public static GameObject MuzzleOfHitParent;     // Muzzle,HitEffect �θ�
    public static GameObject DeadEffectParent;      // DeadEffect �θ�
    // Sound
    public static GameObject SoundP;                // Sound �θ�
    // ����
    public static int SCORE = 0;                    // ������ ���Ǵ� ���ھ�

    static void Init()
    {
        if(instance == null)
        {
            GameObject go = GameObject.Find("@GameManager");
            if(go == null)
            {
                go = new GameObject { name = "@GameManager" };
                go.AddComponent<GameManager>();
            }
            instance = go.GetComponent<GameManager>();
            DontDestroyOnLoad(go);

            CreateDontDestroy(ref PlayerBulletParent,nameof(PlayerBulletParent));
            CreateDontDestroy(ref EnemyObjectParent, nameof(EnemyObjectParent));
            CreateDontDestroy(ref EnemyBulletParent, nameof(EnemyBulletParent));
            CreateDontDestroy(ref MuzzleOfHitParent, nameof(MuzzleOfHitParent));
            CreateDontDestroy(ref DeadEffectParent, nameof(DeadEffectParent));
            CreateDontDestroy(ref ItemObjectParent, nameof(ItemObjectParent));
            CreateDontDestroy(ref SoundP, nameof(SoundP));

            Sound.Init();
        }
    }

    private static void CreateDontDestroy(ref GameObject useObj,string name)
    {
        useObj = new GameObject { name = $"#{name}" };
        DontDestroyOnLoad(useObj);
    }

    #region Observer Pattern
    // -- EndGame Observer
    private List<IEndGameObserver> _endGameObserver = new List<IEndGameObserver>();
    public void EndGame_AddObserver(IEndGameObserver observer)
    {
        _endGameObserver.Add(observer);
    }
    public void EndGame_RemoveObserver(IEndGameObserver observer)
    {
        _endGameObserver.Remove(observer);
    }
    public void EndGame_NoticeObserver()
    {
        for(int i = 0; i < _endGameObserver.Count; ++i)
        {
            _endGameObserver[i].EndGame_Notice();
        }
    }
    #endregion
}
