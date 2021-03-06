using System.Collections;
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
    BackendMain _backend = new BackendMain();
    GoogleManager _google = new GoogleManager();

    public static PlayerManager Player { get => Instance._player; }
    public static ResourceManager Resource { get => Instance._resource; }
    public static PoolManager Pool { get => Instance._pool; }
    public static JsonManager Json { get => Instance._json; }
    public static SoundManager Sound { get => Instance._sound; }
    public static BackendMain BackendMain { get => Instance._backend; }
    public static GoogleManager GoogleM { get => Instance._google; }

    // Player
    public static GameObject PlayerBulletParent;    // Player Weapon 부모

    // Enemy
    public static GameObject EnemyObjectParent;     // Enemy Object 부모
    public static GameObject EnemyBulletParent;     // Enemy Weapon 부모

    // Item
    public static GameObject ItemObjectParent;      // Itme Object 부모

    // 공용
    public static GameObject MuzzleOfHitParent;     // Muzzle,HitEffect 부모
    public static GameObject DeadEffectParent;      // DeadEffect 부모

    // Sound
    public static GameObject SoundP;                // Sound 부모

    // 점수
    public static int SCORE = 0;                    // 점수로 사용되는 스코어

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
            PlayerBulletParent = new GameObject { name = "#PlayerBulletP" };
            DontDestroyOnLoad(PlayerBulletParent);
            EnemyObjectParent = new GameObject { name = "#EnemyObjectP" };
            DontDestroyOnLoad(EnemyObjectParent);
            EnemyBulletParent = new GameObject { name = "#EnemyBulletP" };
            DontDestroyOnLoad(EnemyBulletParent);
            MuzzleOfHitParent = new GameObject { name = "#MuzzleOfHitP" };
            DontDestroyOnLoad(MuzzleOfHitParent);
            DeadEffectParent = new GameObject { name = "#DeadEffectP" };
            DontDestroyOnLoad(DeadEffectParent);
            ItemObjectParent = new GameObject { name = "#ItemP" };
            DontDestroyOnLoad(ItemObjectParent);
            SoundP = new GameObject { name = "#SoundP" };
            DontDestroyOnLoad(SoundP);

            Sound.Init();
        }
    }

    public static void OnUpdate()
    {
        
    }
    
    public static void Clear()
    {

    }


}
