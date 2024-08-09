using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPCMoveAction", menuName = "NPC/NPC Action/NPCMoveAction")]
public class NPCMoveAction : NPCAction
{
    public enum movetype
    {
        randamMove,
        chasingMove,
        targetConversePosMove,
        moveToOrigin
    }

    [Header("�̵� ���� : \n                      ���� �������� �̵�, \n                      Ÿ�� �������� �̵�, \n                      Ÿ�� �ݴ�������� �̵�, \n                      ������ ��ġ�� �̵�")]
    public movetype MoveType;

    public float randamMoveRange;
    public override void Action(NpcController controller)
    {
        switch (MoveType)
        {
            case movetype.randamMove:
                if (controller._agent.remainingDistance < 0.5f)
                {
                    controller._agent.SetDestination(controller.transform.position + new Vector3(Random.Range(-randamMoveRange, randamMoveRange), 0, Random.Range(-randamMoveRange, randamMoveRange)));
                }
                break;
            case movetype.chasingMove:
                break;
            case movetype.targetConversePosMove:
                break;
            case movetype.moveToOrigin:
                break;
        }

    }
}
