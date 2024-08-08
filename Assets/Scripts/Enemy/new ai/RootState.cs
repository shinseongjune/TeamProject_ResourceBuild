using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "rootstate")]
public class RootState : ScriptableObject
{
    public enum npcType
    {
        citizen,
        normalMonster,
        bossMonster
    }
    public npcType eNPC;
    public LayerMask playerlayer;

    [Header("hp")]
    public float maxHp;
    public float attackPower;
    [Header("fov")]
    [Tooltip("시야 거리")]
    public float viewingDistance;
    [Tooltip("시야 각도")]
    public float viewingAngle;
    [Tooltip("곡률")]
    public int segments;
    [Tooltip("공격 범위")]
    public float attackRange;
    [Tooltip("최소 공격 범위")]
    public float attackRangeNear;
}
