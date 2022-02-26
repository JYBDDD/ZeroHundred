using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item ", menuName = "ScriptableItem")]
public class ScriptableObjectC : ScriptableObject
{
    public string ItemName;         // 아이템 이름

    public float ShootRangeUp;      // 발사 속도 Up                  -> 기본 속도 0.5f;
    public int ShootCountUp;        // 발사 갯수 Up                  -> 기본 발사 갯수 1; (미사일에서 사용)
    public int HpUp;                // 체력 회복                     -> (플레이어 체력 회복)

    public int SkillCountUp;        // 플레이어 스킬 갯수 Up          -> 플레이어 스킬 갯수 (최대 3개)
}
