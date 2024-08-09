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
    [Header("�þ� �Ÿ�")]
    public float viewingDistance;
    [Header("�þ� ����")]
    public float viewingAngle;
    [Header("��� (������, ������ ������)")]
    public int segments;
    [Header("���� ���� (�ּ� ���� �������� Ŀ����)")]
    public float attackRange;
    [Header("�ּ� ���� ���� (���� �������� �۾ƾ� ��)")]
    public float attackRangeNear;
}
