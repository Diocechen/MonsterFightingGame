using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius;

    [SerializeField] private LayerMask hitLayer;

    private bool _IsAttacking = false;
    public bool IsAttacking
    {
        get { return _IsAttacking; }
        set { _IsAttacking = value; }
    }

    public void OnAttack()
    {
        IsAttacking = true;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, hitLayer);

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                //print("Player Hit");
                if (collider.gameObject.TryGetComponent<DamageableCharacter>(out var damageableCharacter))
                {
                    CinemachineShake.Instance.ShakeCamera(1f, 0.1f);
                    damageableCharacter.GetDamage(5); //´ú¸Õ§ðÀ»
                    if (transform.localScale.x < 0)
                    {
                        damageableCharacter.OnKnockBack(Vector2.right * 10f);
                    }
                    if (transform.localScale.x > 0)
                    {
                        damageableCharacter.OnKnockBack(-Vector2.right * 10f);
                    }
                }
            }
        }
    }

    public Transform GetAttackPoint()
    {
        return attackPoint; 
    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}
