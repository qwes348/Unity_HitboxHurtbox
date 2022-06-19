using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class Damageable : MonoBehaviour
{
    public UnityEvent<float> onGetDamage;

    public UnityEvent<DamageInfo> onGetDamageFromHitbox;

    public Transform rootTransform;

    public bool IsDead { get => _isDead; set => SetIsDead(value); }

    [SerializeField] private bool _isDead;

    public void GetDamage(float damage)
    {
        onGetDamage.Invoke(damage);
    }

    public void GetDamage(DamageInfo info)
    {
        onGetDamageFromHitbox.Invoke(info);
    }


    public void SetIsDead(bool isDead)
    {
        _isDead = isDead;
    }
}
