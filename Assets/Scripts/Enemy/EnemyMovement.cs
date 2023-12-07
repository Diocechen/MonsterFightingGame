using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform target;
    [SerializeField] private float minimunDistance;

    [SerializeField] private DamageableCharacter damageableCharacter;
    [SerializeField] private EnemyAttack enemyAttack;
    [SerializeField] private EnemyAnimatorScript animatorScript;

    private bool IsWalking = false;
    private Vector2 moveDir;

    [SerializeField] private float AttackRate = 2.0f;
    private float nextAttackTime = 0f;

    void Update()
    {
        if (target == null)
        {
            GameObject obj = GameObject.FindGameObjectsWithTag("Player")[0];
            target = obj.transform;
        }
        
        //²�檺����
        if (target && !damageableCharacter.IsHurt() && !damageableCharacter.IsDead() && !damageableCharacter.IsDizzy())
        {
            moveDir = Vector3.Normalize(transform.position - target.position);
            
            if(moveDir != Vector2.zero)
            {
                if (Vector2.Distance(transform.position, target.position) > minimunDistance)
                {
                    IsWalking = true;
                    if (!damageableCharacter.IsHurt())//�קK����˰ʵe�Ĭ�
                    {
                        Vector2 moveTo = new Vector2(target.position.x, transform.position.y);
                        transform.position = Vector2.MoveTowards(transform.position, moveTo, speed * Time.deltaTime);
                    }
                }
                else
                {
                    //���ӭn�A�s�W�@�Ӹ}���A�ΨӳB�z������ʵe
                    IsWalking = false;
                    if (Time.time >= nextAttackTime)
                    {
                        //�i�����
                        if (!damageableCharacter.IsHurt()) //�קK����˰ʵe�Ĭ�
                        {
                            animatorScript.ExecuteAttackAnimation();
                            nextAttackTime = Time.time + (1.0f / AttackRate);
                        }
                    }
                }
            }
        }
        
    }

    public bool GetIsWalking()
    {
        return IsWalking;
    }

    public float GetDirectionX()
    {
        return moveDir.x;
    }
}
