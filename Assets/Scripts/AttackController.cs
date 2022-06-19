using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class AttackController : MonoBehaviour, Hitbox.IHitboxResponder
{
    public Hitbox weaponHitbox;
    public Animator anim;
    public ThirdPersonController tpsController;
    public StarterAssetsInputs inputs;

    private void Awake()
    {
        if(weaponHitbox == null)
        {
            enabled = false;
            return;
        }

        weaponHitbox.SetResponder(this);
    }

    public void Attack()
    {
        if (!inputs.attack)
            return;

        anim.SetTrigger("Attack");
        inputs.attack = false;
        tpsController.SetCanJump(false);
        tpsController.SetCanMove(false);
    }

    public void OnAttackEnd(AnimationEvent animEvent)
    {
        Debug.Log("AttackEnd");

        anim.ResetTrigger("Attack");
        inputs.attack = false;
        tpsController.SetCanJump(true);
        tpsController.SetCanMove(true);
    }

    public void StartCollision()
    {
        weaponHitbox.StartCheckingCollision();
    }

    public void StopCollision()
    {
        weaponHitbox.StopCheckingCollision();
    }

    public void CollisionedWith(Collider coll)
    {
        Damageable hurtBox = coll.GetComponent<Damageable>();

        if (hurtBox == null)
            return;

        DamageInfo info = new DamageInfo(10f, weaponHitbox.transform.position, weaponHitbox);
        hurtBox.GetDamage(info);
    }
}
