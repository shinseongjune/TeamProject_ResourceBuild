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
    public Transform viewingOriginPos;
    public float viewingDistance;
    public float viewingAngle;
    public int segments;
}
