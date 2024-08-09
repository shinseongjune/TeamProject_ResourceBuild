using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class RagdollSetup : MonoBehaviour
{
    Animator an;
    public Rigidbody[] ragdollRigidbody;
    public Rigidbody hip;
    public PhysicMaterial ragdollMaterial;
    private void Start()
    {
        ragdollRigidbody = GetComponentsInChildren<Rigidbody>();
        an = GetComponent<Animator>();
        DisableRagdoll();
    }
      public void ActivateRagdoll(Collider ragdollCollider)
    {
        an.enabled = false;
        foreach (var rigidbody in ragdollRigidbody)
        {
            rigidbody.isKinematic = false;
            rigidbody.gameObject.GetComponent<Collider>().material= ragdollMaterial;
        }
        if (ragdollCollider != null)
        {
            ragdollCollider.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

    public void DisableRagdoll()
    {
        an.enabled = true;
        foreach (var rigidbody in ragdollRigidbody)
        {
            rigidbody.isKinematic = true;
        }
    }
}
