using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MobPackSystem : MonoBehaviour
{
    public LayerMask playerLayer;
    [HideInInspector]public List<EnemyAi> enemys;
    private Node _behaviorTree;
    public bool iscombatState;

    public float combatCancelRange;

    void Start()
    {
        enemys.Clear();
        // �θ� ������Ʈ�� �ڽ� ������ŭ �ݺ�
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);

            enemys.Add(child.GetComponentInChildren<EnemyAi>());
        }
        _behaviorTree = InitializeBehaviorTree();
    }

    private Node InitializeBehaviorTree()
    {
        return new Selector(new List<Node> {
            new Sequence(new List<Node> { // ���� ������
                new Condition(() => detection() || iscombatState),
                new ActionNode(() => RemindState()),
            }),
            new Sequence(new List<Node> { // ������ �Ҵ� ������
                new Condition(() => NoAttackers()),
                new ActionNode(() => AttackerSelection()),
            }),
        });
    }
    void Update()
    {
        _behaviorTree.Tick();
    }

 
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue; // ���� ������ ����
        Gizmos.DrawWireSphere(transform.position, combatCancelRange);
    }


    //Behavior Tree Function
    #region


    bool detection()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 4, playerLayer);

        foreach (Collider2D collider in colliders)
        {
            return true;
        }
        return false;
    }
    bool NoAttackers()
    {
        return false;
    }
    void AttackerSelection()
    {
        for (int i = 0; i < enemys.Count; i++)
        {

        }
    }
    void RemindState()
    { 
    
    }
    #endregion
}
