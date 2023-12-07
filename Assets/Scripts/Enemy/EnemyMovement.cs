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
        
        //簡單的移動
        if (target && !damageableCharacter.IsHurt() && !damageableCharacter.IsDead() && !damageableCharacter.IsDizzy())
        {
            moveDir = Vector3.Normalize(transform.position - target.position);
            
            if(moveDir != Vector2.zero)
            {
                if (Vector2.Distance(transform.position, target.position) > minimunDistance)
                {
                    IsWalking = true;
                    if (!damageableCharacter.IsHurt())//避免跟受傷動畫衝突
                    {
                        Vector2 moveTo = new Vector2(target.position.x, transform.position.y);
                        transform.position = Vector2.MoveTowards(transform.position, moveTo, speed * Time.deltaTime);
                    }
                }
                else
                {
                    //應該要再新增一個腳本，用來處理攻擊跟動畫
                    IsWalking = false;
                    if (Time.time >= nextAttackTime)
                    {
                        //進行攻擊
                        if (!damageableCharacter.IsHurt()) //避免跟受傷動畫衝突
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
