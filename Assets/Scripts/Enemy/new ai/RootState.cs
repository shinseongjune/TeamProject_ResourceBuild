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

    public float maxHp;
    public float attackPower;

}
