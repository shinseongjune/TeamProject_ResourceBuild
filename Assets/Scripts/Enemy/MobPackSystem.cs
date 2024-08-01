using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobPackSystem : IUpdater
{
    public LayerMask playerLayer;
    public float basicBetectionRange;

    public void Update()
    { 
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, basicBetectionRange, playerLayer);

        foreach (Collider2D collider in colliders)
        {
            Debug.Log("Player detected: " + collider.gameObject.name);
        }
        throw new System.NotImplementedException(); // IUpdater의 Update함수입니다.
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position,basicBetectionRange);
    }
  
}
