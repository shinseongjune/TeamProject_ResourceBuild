using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RootState", menuName = "NPC/RootState")]
public class RootState : ScriptableObject
{
 
    public LayerMask playerlayer;

    [Header("hp")]
    public float maxHp;
    public float attackPower;
    [Header("시야 거리")]
    public float viewingDistance;
    [Header("시야 각도")]
    public float viewingAngle;
    [Header("곡률 (디버깅용, 많으면 안좋음)")]
    public int segments;
    [Header("공격 범위 (최소 공격 범위보다 커야함)")]
    public float attackRange;
    [Header("최소 공격 범위 (공격 범위보다 작아야 됨)")]
    public float attackRangeNear;
}
