using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorScript : AnimatorScript
{
    private const string IS_WALKING = "IsWalking";
    private const string ATTACK1 = "Attack1";
    private const string ATTACK2 = "Attack2";
    private const string SKILL1 = "Skill1";
    private const string HURT = "Hurt";
    private const string DEAD = "Dead";

    [SerializeField] private Animator animator;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerAttack playerAttack;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private DamageableCharacter damageableCharacter;

    private void Awake()
    {
        gameInput.OnAttackAction += GameInput_OnAttackAction;
    }

    private void GameInput_OnAttackAction(object sender, AttackTypes e)
    {
        if(e.inputType == 1)
        {
            animator.SetTrigger(ATTACK1);
        }
        else if (e.inputType == 2)
        {
            animator.SetTrigger(ATTACK2);
        }else if (e.inputType == 3)
        {
            animator.SetTrigger(SKILL1);
        }
    }

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (playerMovement.GetDirectionX() > 0)
        {
            transform.parent.localScale = new Vector3(1, 1, 1);
        }else if (playerMovement.GetDirectionX() < 0)
        {
            transform.parent.localScale = new Vector3(-1, 1, 1);
        }

        animator.SetBool(IS_WALKING, playerMovement.GetIsWalking());
        //animator.SetBool(ATTACK1, playerAttack.GetExecuteAttack1());
        //animator.SetBool(ATTACK2, playerAttack.GetExecuteAttack2());
    }

    //for animation
    public void Attack1Sound()
    {
        AudioManagerScript.instance.PlayAudio(1, "sePlayerAttack1", false);
    }
    public void ExecuteOnAttack1()
    {
        playerAttack.OnAttack1();
        damageableCharacter.Invincible(true);
    }

    public void Attack2Sound()
    {
        AudioManagerScript.instance.PlayAudio(1, "sePlayerAttack2", false);
    }
    public void ExecuteOnAttack2()
    {
        playerAttack.OnAttack2();
        damageableCharacter.Invincible(true);
    }

    public void SetToNotAttacking()
    {
        playerAttack.IsAttacking = false;
        damageableCharacter.Invincible(false);
    }

    public override void ExecuteDeadAnimation()
    {
        animator.SetBool(DEAD, true);
    }

    public void Death()
    {
        damageableCharacter.Death();
    }

    public void CameraShakeWhileWalking()
    {
        System.Random obj = new();
        int n = obj.Next(1, 5);

        switch (n)
        {
            case 1:
                AudioManagerScript.instance.PlayAudio(2, "seFootStep1", false);
                break;
            case 2:
                AudioManagerScript.instance.PlayAudio(2, "seFootStep2", false);
                break;
            case 3:
                AudioManagerScript.instance.PlayAudio(2, "seFootStep3", false);
                break;
            case 4:
                AudioManagerScript.instance.PlayAudio(2, "seFootStep4", false);
                break;
        }
        CinemachineShake.Instance.ShakeCamera(0.7f, 0.1f);
    }

    public void Skill1Sound1()
    {
        AudioManagerScript.instance.PlayAudio(1, "sePlayerSkill1", false);
    }
    public void Skill1Sound2()
    {
        AudioManagerScript.instance.PlayAudio(1, "sePlayerSkill2", false);
    }
    public void ExecuteSkill1()
    {
        playerAttack.OnSkill1();
    }

    public void EndSkill1()
    {
        playerAttack.Skill1End();
    }

    public override void ExecuteHurtAnimation()
    {
        animator.SetTrigger(HURT);
    }
    public void ExecuteRecovery()
    {
        damageableCharacter.Recovery();
    }
}
