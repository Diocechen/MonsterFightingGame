using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimatorScript : AnimatorScript
{
    [SerializeField] private Animator animator;
    [SerializeField] private EnemyMovement enemyMovement;
    [SerializeField] private EnemyAttack enemyAttack;
    [SerializeField] private DamageableCharacter damageableCharacter;
    [SerializeField] private DropLoot dropLoot;

    private const string IS_WALKING = "IsWalking";
    private const string ATTACK = "Attack";
    private const string HURT = "Hurt";
    private const string DEAD = "Dead";
    private const string DIZZY = "Dizzy";

    private void Update()
    {
        if (enemyMovement.GetDirectionX() > 0)
        {
            transform.parent.localScale = new Vector3(1, 1, 1);
        }
        else if (enemyMovement.GetDirectionX() < 0)
        {
            transform.parent.localScale = new Vector3(-1, 1, 1);
        }

        animator.SetBool(IS_WALKING, enemyMovement.GetIsWalking());
    }

    public override void ExecuteHurtAnimation()
    {
        //print("Hurt");
        AudioManagerScript.instance.PlayAudio(5, "seEnemyGetHit", false);
        animator.SetTrigger(HURT);
    }

    public override void ExecuteDeadAnimation()
    {
        animator.SetBool(DEAD, true);
    }
    public void FootStep()
    {
        AudioManagerScript.instance.PlayAudio(2, "seFootStep2", false);
    }
    public void ExecuteDropLoot()
    {
        System.Random obj = new();
        int rand = obj.Next(0, 100);
        if (rand > 50)
        {
            dropLoot.Drop();
        }
    }
    public void Death()
    {
        damageableCharacter.Death();
    }

    public void ExecuteAttackAnimation()
    {
        animator.SetTrigger(ATTACK);
    }

    public void AttackSound()
    {
        AudioManagerScript.instance.PlayAudio(3, "seEnemyAttack", false);
    }

    public void ExecuteOnAttack() 
    {
        enemyAttack.OnAttack();
    }

    public void ExecuteRecovery() 
    {
        //print("Recovery");
        damageableCharacter.Recovery();
    }

    public void CheckDizzy()
    {
        if (damageableCharacter.IsDizzy())
        {
            animator.SetTrigger(DIZZY);
        }
    }

    public void OnRecoverDizzy()
    {
        damageableCharacter.RecoverDizzy();
    }
}
