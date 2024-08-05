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

    public bool testActivateRagdoll;
    public Vector3 testvelocity;
    public bool testDisableRagdoll;

    private void Start()
    {
        ragdollRigidbody = GetComponentsInChildren<Rigidbody>();
        an = GetComponent<Animator>(); 
    }
    private void Update()
    {
        if (testActivateRagdoll)
        {
            ActivateRagdoll();
        }
        if (testDisableRagdoll)
        {
            DisableRagdoll();
        }
    }
    public void ActivateRagdoll()
    {
        an.enabled = false;
        hip.velocity = testvelocity;
        foreach (var rigidbody in ragdollRigidbody)
        {
            rigidbody.isKinematic = false;
            rigidbody.gameObject.GetComponent<Collider>().material= ragdollMaterial;
        }
        testActivateRagdoll = false;
        hip.velocity = testvelocity;
    }

    public void DisableRagdoll()
    {
        an.enabled = true;
        foreach (var rigidbody in ragdollRigidbody)
        {
            rigidbody.isKinematic = true;
        }
        testDisableRagdoll = false;
    }
}
