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
    [Tooltip("�þ� �Ÿ�")]
    public float viewingDistance;
    [Tooltip("�þ� ����")]
    public float viewingAngle;
    [Tooltip("���")]
    public int segments;
    [Tooltip("���� ����")]
    public float attackRange;
    [Tooltip("�ּ� ���� ����")]
    public float attackRangeNear;
}
