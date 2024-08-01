using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : AttackInteractable
{
    public override void Attack()
    {
        throw new System.NotImplementedException();
    }
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }
    public override void Die()
    {
        throw new System.NotImplementedException();
    }
}
