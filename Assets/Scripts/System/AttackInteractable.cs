using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackInteractable : MonoBehaviour
{
    public float hp;


    public abstract void Attack();

    public virtual void TakeDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Die();
        }
    }

    public abstract void Die();
}
