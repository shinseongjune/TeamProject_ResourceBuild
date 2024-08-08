using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "NPCBehaviourTreeBuilder")]
public class NPCBehaviourTreeBuilder : ScriptableObject
{
    public enum npctype
    {
        Citizen,
        NormalMonster,    // 오타 수정
        EliteMonster,
        MiddleBoss,
        BossMonster       // 오타 수정
    }

    [Header("NPC 종류 : 시민, 일반 몬스터, 보스 몬스터")]
    public npctype NPCType;

    [HideInInspector] public Node _behaviorTree;

    public Node InitializeBehaviorTree(NpcController controller)
    {
        switch (NPCType)
        {
            case npctype.Citizen:
                return new Selector(new List<Node> {
                    new Sequence(new List<Node> {
                        new Condition(() => controller._stat.hp <= 0),
                        new ActionNode(() => DeathAction.Action(controller)),
                    }),
                    new Sequence(new List<Node> {
                        new Condition(() => controller._stat.hp < controller._stat.Maxhp),
                        new ActionNode(() => RunawayAction.Action(controller)),
                    }),
                    new ActionNode(() => WalkingAction.Action(controller))
                });
            case npctype.NormalMonster:
                return new Selector(new List<Node> {
                    new Sequence(new List<Node> {
                        new Condition(() => controller._stat.hp <= 0),
                        new ActionNode(() => DeathAction.Action(controller)),
                    }),
                    new ActionNode(() => WalkingAction.Action(controller))
                });
            case npctype.EliteMonster:
                return new Selector(new List<Node> {
                    new Sequence(new List<Node> {
                        new Condition(() => controller._stat.hp <= 0),
                        new ActionNode(() => DeathAction.Action(controller)),
                    }),
                    new ActionNode(() => WalkingAction.Action(controller))
                });
            case npctype.MiddleBoss:
                return new Selector(new List<Node> {
                    new Sequence(new List<Node> {
                        new Condition(() => controller._stat.hp <= 0),
                        new ActionNode(() => DeathAction.Action(controller)),
                    }),
                    new ActionNode(() => WalkingAction.Action(controller))
                });
            case npctype.BossMonster:
                return new Selector(new List<Node> {
                    new Sequence(new List<Node> {
                        new Condition(() => controller._stat.hp <= 0),
                        new ActionNode(() => DeathAction.Action(controller)),
                    }),
                    new ActionNode(() => WalkingAction.Action(controller))
                });
            default:
                return null;  // 모든 경우에 대해 반환값이 없는 문제를 방지하기 위해 추가
        }
    }

    // Citizen
    public int Citizen;
    public NPCAction DeathAction;
    public NPCAction WalkingAction;
    public NPCAction RunawayAction;
    // NormalMonster
    public int NormalMonster;    
    // EliteMonster
    public int EliteMonster;
    // MiddleBoss
    public int MiddleBoss;
    // BossMonster
    public int BossMonster;     
}

[CustomEditor(typeof(NPCBehaviourTreeBuilder))]
public class NPCBehaviourTreeBuilderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // 스크립트 참조
        NPCBehaviourTreeBuilder exampleScript = (NPCBehaviourTreeBuilder)target;

        // Enum 필드 그리기
        exampleScript.NPCType = (NPCBehaviourTreeBuilder.npctype)EditorGUILayout.EnumPopup("NPC Type", exampleScript.NPCType);

        // Enum 값에 따라 다른 변수 표시
        switch (exampleScript.NPCType)
        {
            case NPCBehaviourTreeBuilder.npctype.Citizen:
                EditorGUILayout.Space(10);
                EditorGUILayout.LabelField("일반 시민의 행동 정의하기");
                EditorGUILayout.Space(10);
                exampleScript.Citizen = EditorGUILayout.IntField("Citizen Value", exampleScript.Citizen);
                exampleScript.DeathAction = (NPCAction)EditorGUILayout.ObjectField("Death Action", exampleScript.DeathAction, typeof(NPCAction), false);
                exampleScript.RunawayAction = (NPCAction)EditorGUILayout.ObjectField("Runaway Action", exampleScript.RunawayAction, typeof(NPCAction), false);
                exampleScript.WalkingAction = (NPCAction)EditorGUILayout.ObjectField("Walking Action", exampleScript.WalkingAction, typeof(NPCAction), false);
                break;
            case NPCBehaviourTreeBuilder.npctype.NormalMonster:
                exampleScript.NormalMonster = EditorGUILayout.IntField("Normal Monster Value", exampleScript.NormalMonster);
                break;
            case NPCBehaviourTreeBuilder.npctype.EliteMonster:
                exampleScript.EliteMonster = EditorGUILayout.IntField("Elite Monster Value", exampleScript.EliteMonster);
                break;
            case NPCBehaviourTreeBuilder.npctype.MiddleBoss:
                exampleScript.MiddleBoss = EditorGUILayout.IntField("Middle Boss Value", exampleScript.MiddleBoss);
                break;
            case NPCBehaviourTreeBuilder.npctype.BossMonster:
                exampleScript.BossMonster = EditorGUILayout.IntField("Boss Monster Value", exampleScript.BossMonster);
                break;
        }

        // 변경 사항이 있을 경우 Inspector 업데이트
        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
        }
    }
}
