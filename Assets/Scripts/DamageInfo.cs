using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageInfo
{
    public float damage;
    public Vector3 damagePos;
    public Hitbox attackerHitbox;

    public DamageInfo(float damage, Vector3 damagePos, Hitbox attackerHitbox)
    {
        this.damage = damage;
        this.damagePos = damagePos;
        this.attackerHitbox = attackerHitbox;
    }
}
