using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "NPCBehaviourTreeBuilder", menuName = "NPC/NPCBehaviourTreeBuilder")]
public class NPCBehaviourTreeBuilder : ScriptableObject
{
    public enum npctype
    {
        Citizen,
        NormalMonster,   
        BossMonster      
    }

    [Header("NPC ���� : �ù�, �Ϲ� ����, ���� ����")]
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
                        new Condition(() => !controller.Spawner.PlayerDetection()),
                        new ActionNode(() => DeathAction.Action(controller)),
                    }),
                    new Sequence(new List<Node> {
                        new Condition(() => controller._stat.hp <= 0),
                        new ActionNode(() => DeathAction.Action(controller)),
                    }),
                    new Sequence(new List<Node> {
                        new Condition(() => controller.FieldOfViewDetection()),
                        new Selector(new List<Node> {
                            new Condition(() => strikeFirst),
                            new ActionNode(() => DeathAction.Action(controller)),
                            new Condition(() => !strikeFirst),
                            new ActionNode(() => DeathAction.Action(controller))
                        })
                    }),
                    new Sequence(new List<Node> {
                        new Condition(() => controller._stat.hp <= 0),
                        new ActionNode(() => DeathAction.Action(controller)),
                    }),

                });
            case npctype.BossMonster:
                return new Selector(new List<Node> {
                    new Sequence(new List<Node> {
                        new Condition(() => controller._stat.hp <= 0),
                        new ActionNode(() => DeathAction.Action(controller)),
                    }),
                    new Sequence(new List<Node> {
                        new Condition(() => controller.FieldOfViewDetection()),
                        new ActionNode(() => DeathAction.Action(controller))
                    }),
                    new Sequence(new List<Node> {
                        new Condition(() => controller._stat.hp <= 0),
                        new ActionNode(() => DeathAction.Action(controller)),
                    }),
                });
            default:
                return null;  
        }
    }

    // Citizen
    public NPCAction DeathAction;
    public NPCAction WalkingAction;
    public NPCAction RunawayAction;
    // NormalMonster
    public int NormalMonster;    
    public bool strikeFirst;
    // EliteMonster
    public int BossMonster;     
}

[CustomEditor(typeof(NPCBehaviourTreeBuilder))]
public class NPCBehaviourTreeBuilderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // ��ũ��Ʈ ����
        NPCBehaviourTreeBuilder exampleScript = (NPCBehaviourTreeBuilder)target;

        // Enum �ʵ� �׸���
        exampleScript.NPCType = (NPCBehaviourTreeBuilder.npctype)EditorGUILayout.EnumPopup("NPC Type", exampleScript.NPCType);

        // Enum ���� ���� �ٸ� ���� ǥ��
        switch (exampleScript.NPCType)
        {
            case NPCBehaviourTreeBuilder.npctype.Citizen:
                EditorGUILayout.Space(10);
                EditorGUILayout.LabelField("�Ϲ� �ù��� �ൿ �����ϱ�");
                EditorGUILayout.Space(10);
                exampleScript.DeathAction = (NPCAction)EditorGUILayout.ObjectField("Death Action", exampleScript.DeathAction, typeof(NPCAction), false);
                exampleScript.RunawayAction = (NPCAction)EditorGUILayout.ObjectField("Runaway Action", exampleScript.RunawayAction, typeof(NPCAction), false);
                exampleScript.WalkingAction = (NPCAction)EditorGUILayout.ObjectField("Walking Action", exampleScript.WalkingAction, typeof(NPCAction), false);
                break;
            case NPCBehaviourTreeBuilder.npctype.NormalMonster:
                exampleScript.NormalMonster = EditorGUILayout.IntField("Normal Monster Value", exampleScript.NormalMonster);
                exampleScript.strikeFirst = EditorGUILayout.Toggle("Strike First", exampleScript.strikeFirst);
                break;
            case NPCBehaviourTreeBuilder.npctype.BossMonster:
                exampleScript.BossMonster = EditorGUILayout.IntField("Boss Monster Value", exampleScript.BossMonster);
                break;
        }

        // ���� ������ ���� ��� Inspector ������Ʈ
        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
        }
    }
}
