using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NPC Action", menuName = "NPC Action/NPCMoveAction")]
public class NPCMoveAction : NPCAction
{
    public override void Action(NpcController controller)
    {
        controller.SetPos();
    }
}
