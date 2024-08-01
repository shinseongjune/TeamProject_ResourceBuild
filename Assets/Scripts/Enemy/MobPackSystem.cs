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
        // 부모 오브젝트의 자식 개수만큼 반복
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
            new Sequence(new List<Node> { // 감지 시퀀스
                new Condition(() => detection() || iscombatState),
                new ActionNode(() => RemindState()),
            }),
            new Sequence(new List<Node> { // 공격자 할당 시퀀스
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
        Gizmos.color = Color.blue; // 감지 범위의 색상
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
