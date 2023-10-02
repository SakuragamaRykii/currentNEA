using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKillable
{
    public abstract void TakeDamage(int amount);
    public abstract bool IsDead();
    public abstract void Die();
}
