using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcController : MonoBehaviour
{
    [HideInInspector] public EnemyStat _stat;
    [HideInInspector] public NavMeshAgent _agent;
    [HideInInspector] public RagdollSetup ragdoll;
    public MosterSpawner Spawner;
    public RootState rootState;
    public Transform viewingOriginPos;
    public List<NPCSkill> skills;
    public NPCBehaviourTreeBuilder BehaviourTree;

    private Node nodeTree;

    private void Start()
    {
        nodeTree = BehaviourTree.InitializeBehaviorTree(this);
        _agent = GetComponent<NavMeshAgent>();
        ragdoll = GetComponent<RagdollSetup>();
        _stat = GetComponent<EnemyStat>();
    }
    
    private void Update()
    {
        nodeTree.Tick();
    }

    void OnEnable()
    { 

    }
    
    public void SetPos(Vector3 pos)
    {
        _agent.SetDestination(pos);
    }
    public bool FieldOfViewDetection()
    {
        Collider[] playersInViewRadius = Physics.OverlapSphere(viewingOriginPos.position, rootState.viewingDistance, rootState.playerlayer);

        foreach (Collider player in playersInViewRadius)
        {
            Vector3 directionToPlayer = (player.transform.position - viewingOriginPos.position).normalized;
            float angleToPlayer = Vector3.Angle(viewingOriginPos.forward, directionToPlayer); // transform.forward를 사용하여 방향 벡터를 확인합니다.

            if (angleToPlayer < rootState.viewingAngle / 2)  // 각도가 반각도보다 작으면 감지
            {
                // 플레이어가 시야각 내에 있는 경우
                Debug.DrawLine(viewingOriginPos.position, player.transform.position, Color.white);

                return true;
            }
        }
        return false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green; // 최대 공격 범위의 색상
        Gizmos.DrawWireSphere(transform.position, rootState.attackRange);
        Gizmos.color = Color.cyan; // 최소 공격 범위의 색상
        Gizmos.DrawWireSphere(transform.position, rootState.attackRangeNear);

        Gizmos.color = Color.white;
        DrawArc(viewingOriginPos.position,
            viewingOriginPos.position + Quaternion.Euler(0, rootState.viewingAngle / 2, 0) * viewingOriginPos.forward * rootState.viewingDistance,
            viewingOriginPos.position + Quaternion.Euler(0, -rootState.viewingAngle / 2, 0) * viewingOriginPos.forward * rootState.viewingDistance,
            rootState.viewingDistance);

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
        int segments = Mathf.Max(2, this.rootState.segments);
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
