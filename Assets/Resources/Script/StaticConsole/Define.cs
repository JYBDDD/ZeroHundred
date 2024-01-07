using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum Sound
    {
        bgm,
        effect,
        maxCount,
    }

    public enum ObjType
    {
        None,
        Enemy,
        Player,
        Boss,
    }
}

namespace Path
{
    public struct UI_P
    {
        public const string UISuccess = "Art/Sound/Effect/UI/UISuccess";
        public const string UIFail = "Art/Sound/Effect/UI/UIFail";
        public const string UITap = "Art/Sound/Effect/UI/UITap";
    }

    public class ObjSound_P
    {
        // Player
        public const string PlayerDie = "Art/Sound/Effect/Player/PlayerDie";

        // Enemy
        public const string EnemyDie = "Art/Sound/Effect/Enemy/EnemyDie/EnemyDie";
        public const string Enemy5Move = "Art/Sound/Effect/Enemy/Enemy5Move/Enemy5MoveSound";
        public const string EnemyMissileExplosion = "Art/Sound/Effect/Enemy/EnemyMissile/EnemyMissileExplosion";
        public const string EnemyMisslieShot = "Art/Sound/Effect/Enemy/EnemyMissile/EnemyMissileShot";
        public const string BombingExplosion = "Art/Sound/Effect/Enemy/Bombing/BombingExplosion";

        // Boss
        public const string FlashBangRinging = "Art/Sound/Effect/Enemy/Boss/FlashBangRinging";
        public const string FlashBang = "Art/Sound/Effect/Enemy/Boss/FlashBang";
        public const string BossRocketShot = "Art/Sound/Effect/Enemy/Boss/BossRoketShot";
        public const string BossAroundShot = "Art/Sound/Effect/Enemy/Boss/BossAroundShot";
        public const string BossDie = "Art/Sound/Effect/Enemy/Boss/BossDie";

        // Item
        public const string ItemGet = "Art/Sound/Effect/Item/ItemGet";
    }

    public struct SceneSound_P
    {
        public const string StartSceneBGM = "Art/Sound/BGM/StartScene_BGM";
        public const string LoadingSceneBGM = "Art/Sound/BGM/LoadingScene_BGM";
        public const string GameSceneBGM = "Art/Sound/BGM/GameScene_BGM";
        public const string BossBGM = "Art/Sound/BGM/Boss_BGM";
    }
}

namespace SceneN
{
    public struct SceneName
    {
        public const string LoginScene = "LoginScene";
        public const string LobbyScene = "LobbyScene";
        public const string GameScene = "GameScene";
    }

}
