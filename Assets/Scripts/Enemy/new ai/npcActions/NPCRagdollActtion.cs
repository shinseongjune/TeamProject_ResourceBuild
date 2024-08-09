using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NPCRagdollActtion", menuName = "NPC/NPC Action/NPCRagdollActtion")]
public class NPCRagdollActtion : NPCAction
{
    public override void Action(NpcController controller)
    {
        controller.ragdoll.ActivateRagdoll(null);
        
    }
}
