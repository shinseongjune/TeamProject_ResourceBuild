using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    MobPackSystem _mobPackSystem;
    Node _behaviorTree;
    EnemyStat _stat;
    NavMeshAgent agent;
    RagdollSetup ragdoll;

    Transform originPos;

    public float attackRange;
    public float attackRangeNear;

    public Transform viewingOriginPos;
    public float viewingDistance;
    public float viewingAngle;
    public int segments;

    public bool isattacker;

    private void Start()
    {
        _mobPackSystem = GetComponentInParent<MobPackSystem>();
        _behaviorTree = InitializeBehaviorTree();
        _stat = GetComponent<EnemyStat>();
        originPos = transform;
    }
    private Node InitializeBehaviorTree()
    {
        return new Selector(new List<Node> {
            new Sequence(new List<Node> { // 사망 시퀀스
                new Condition(() => _stat.hp <= 0),
                new ActionNode(() => Death()),
            }),
            new Sequence(new List<Node> {// 공격자?
                new Condition(() => _mobPackSystem.iscombatState),
                new Sequence(new List<Node> { 
                    new Condition(() => isattacker),
                    new ActionNode(() => AttackerAction()),
                }),
                new Sequence(new List<Node> {
                    new Condition(() => !isattacker),
                    new ActionNode(() => CombatWait()),
                }),
            }),
            new Sequence(new List<Node> { // 감지 시퀀스
                new Condition(() => FieldOfViewDetection()),
                new ActionNode(() => FieldOfViewDetection()),
            }),
        });
    }
    private void Update()
    {
        _behaviorTree.Tick();
    }

    void Death()
    { 
        
    }

    bool FieldOfViewDetection()
    {
        Collider[] playersInViewRadius = Physics.OverlapSphere(viewingOriginPos.position, viewingDistance, _mobPackSystem.playerLayer);

        foreach (Collider player in playersInViewRadius)
        {
            Vector3 directionToPlayer = (player.transform.position - viewingOriginPos.position).normalized;
            float angleToPlayer = Vector3.Angle(viewingOriginPos.forward, directionToPlayer); // transform.forward를 사용하여 방향 벡터를 확인합니다.

            if (angleToPlayer < viewingAngle / 2)  // 각도가 반각도보다 작으면 감지
            {
                // 플레이어가 시야각 내에 있는 경우
                Debug.DrawLine(viewingOriginPos.position, player.transform.position, Color.white);
                
                return true;
            }
        }
        return false;
    }

    void AttackerAction()
    { 

    }
    void CombatWait()
    { 
    }
    void Patrol()
    { 
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green; // 최대 공격 범위의 색상
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.cyan; // 최소 공격 범위의 색상
        Gizmos.DrawWireSphere(transform.position, attackRangeNear);

        Gizmos.color = Color.white;
            DrawArc(viewingOriginPos.position,
                viewingOriginPos.position + Quaternion.Euler(0, viewingAngle / 2, 0) * viewingOriginPos.forward * viewingDistance,
                viewingOriginPos.position + Quaternion.Euler(0, -viewingAngle / 2, 0) * viewingOriginPos.forward * viewingDistance,
                viewingDistance);

    }
    void DrawArc(Vector3 center, Vector3 start, Vector3 end, float radius)
    {
        Gizmos.DrawLine(center, start);
        Gizmos.DrawLine(center, end);
        // 원호를 그리기 위한 각도 계산
        Vector3 startToCenter = (start - center).normalized;
        Vector3 endToCenter = (end - center).normalized;

        float startAngle = Mathf.Atan2(startToCenter.z, startToCenter.x) * Mathf.Rad2Deg;
        float endAngle = Mathf.Atan2(endToCenter.z, endToCenter.x) * Mathf.Rad2Deg;

        if (endAngle < startAngle)
        {
            endAngle += 360;
        }

        // 원호를 그리기 위해 각도 범위에 따라 점 계산
        int segments = Mathf.Max(2, this.segments);
        Vector3 previousPoint = center + radius * new Vector3(Mathf.Cos(startAngle * Mathf.Deg2Rad), 0, Mathf.Sin(startAngle * Mathf.Deg2Rad));

        for (int i = 1; i <= segments; i++)
        {
            float angle = Mathf.Lerp(startAngle, endAngle, i / (float)segments);
            Vector3 point = center + radius * new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad));
            Gizmos.DrawLine(previousPoint, point);
            previousPoint = point;
        }
    }
}
