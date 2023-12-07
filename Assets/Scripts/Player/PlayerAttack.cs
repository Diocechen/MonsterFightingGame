using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    
    [SerializeField] private GameInput gameInput;

    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius;

    [SerializeField] private Transform skill1ExecutePoint;
    [SerializeField] private LineRenderer lineRenderer;

    [SerializeField] private LayerMask hitLayer;

    private float nextSkillUpdateTime = 0f;
    private float skillExecuteRate = 5f;

    private bool _IsAttacking = false;
    public bool IsAttacking
    {
        get { return _IsAttacking; }
        set { _IsAttacking = value; }
    }


    private void Awake()
    {
        //gameInput.OnAttackAction += GameInput_OnAttackAction; //�q�\OnAttackAction
    }

    private void Start()
    {
        Skill1End();
    }

    private void Update()
    {
        if (lineRenderer.isVisible)
        {
            if (Time.time > nextSkillUpdateTime)
            {
                if (transform.localScale.x > 0)
                {
                    Skill1Update(skill1ExecutePoint.position, new Vector2(transform.localScale.x + 10000, transform.position.y));
                }
                else if (transform.localScale.x < 0)
                {
                    Skill1Update(skill1ExecutePoint.position, new Vector2(-transform.localScale.x - 10000, transform.position.y));
                }
                nextSkillUpdateTime = Time.time + (1.0f / skillExecuteRate);
            }
        }
    }
    //�ثe�i�H���ΥΨ�o�Ӥ覡Ĳ�o���� GameInput -> AnimatiorScript -> Attack
    /*private void GameInput_OnAttackAction(object sender, System.EventArgs e)
    {
        //
        Debug.Log("input");

        //�����Ҧ��IĲ������
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, hitLayer);

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.CompareTag("Enemy"))
            {
                Debug.Log("hit enemy");
            }
        }
    }*/

    //�]���ʵeevent����Ǧ��Ѽƪ�function ����A�Q�Q���S����n����k
    public void OnAttack1() //�ʵeĲ�o
    {
        IsAttacking = true;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, hitLayer);

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.CompareTag("Enemy"))
            {
                if (collider.gameObject.TryGetComponent<DamageableCharacter>(out var damageableCharacter))
                {
                    CinemachineShake.Instance.ShakeCamera(3f, 0.1f);
                    HandleParticle.instance.SummonParticle(0, collider.transform.position);
                    damageableCharacter.GetDamage(10); //���է���
                    damageableCharacter.OnDizzy();
                    if (transform.localScale.x > 0)
                    {
                        damageableCharacter.OnKnockBack(Vector2.right * 10f);
                    }
                    if (transform.localScale.x < 0)
                    {
                        damageableCharacter.OnKnockBack(-Vector2.right * 10f);
                    }
                }
            }
        }
    }

    public void OnAttack2() //�ʵeĲ�o
    {
        IsAttacking = true;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, hitLayer);

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.CompareTag("Enemy"))
            {
                if (collider.gameObject.TryGetComponent<DamageableCharacter>(out var damageableCharacter))
                {
                    CinemachineShake.Instance.ShakeCamera(3f, 0.1f);
                    HandleParticle.instance.SummonParticle(0, collider.transform.position);
                    damageableCharacter.GetDamage(20); //���է���
                    if (transform.localScale.x > 0)
                    {
                        damageableCharacter.OnKnockBack(Vector2.right * 50f);
                    }
                    if (transform.localScale.x < 0)
                    {
                        damageableCharacter.OnKnockBack(-Vector2.right * 50f);
                    }
                }
            }
        }
    }

    public void OnSkill1()
    {
        IsAttacking = true;
        CinemachineShake.Instance.ShakeCamera(3f, 1.5f);
        lineRenderer.enabled = true;
    }

    public void Skill1Update(Vector2 start, Vector2 direction)
    {
        RaycastHit2D[] raycastHits = Physics2D.RaycastAll(start, direction, Mathf.Infinity);
        foreach (RaycastHit2D hit in raycastHits)
        {
            Debug.DrawLine(start, hit.transform.position, Color.red, 1f);
            if (hit.rigidbody && hit.rigidbody.gameObject.CompareTag("Enemy"))
            {
                if (hit.rigidbody.gameObject.TryGetComponent<DamageableCharacter>(out var damageableCharacter))
                {
                    HandleParticle.instance.SummonParticle(0, hit.transform.position);
                    damageableCharacter.GetDamage(15);
                    if (transform.localScale.x > 0)
                    {
                        damageableCharacter.OnKnockBack(Vector2.right * 10f);
                    }
                    if (transform.localScale.x < 0)
                    {
                        damageableCharacter.OnKnockBack(-Vector2.right * 10f);
                    }
                }
            }
        }
    }

    public void Skill1End()
    {
        lineRenderer.enabled = false;
    }

    //Gizmos�O�@�إΩ�bScene���Ϥ�ø�s���U�u���B�ϧΩM��L�i���Ƥ������u��C
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }

}

