using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float Speed = 7f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private PlayerAttack playerAttack;
    [SerializeField] private DamageableCharacter damageableCharacter;

    [SerializeField] private float limitR;
    [SerializeField] private float limitL;

    private bool IsWalking;

    private Vector2 moveDir;

    void Update()
    {
        if (transform.position.x >= limitL && transform.position.x <= limitR)
        {
            if (!damageableCharacter.IsDead() && !playerAttack.IsAttacking && !damageableCharacter.IsHurt())
            {
                moveDir = gameInput.GetMovementVector2D(); //透過gameInput獲得輸入值

                if (moveDir != null && moveDir != Vector2.zero)
                {
                    IsWalking = true;
                    float SpeedX = moveDir.x * Time.deltaTime * Speed;
                    transform.position += new Vector3(SpeedX, 0f, 0f);
                }
                else
                {
                    IsWalking = false;
                }
            }
        }
        else //可能會跑出一點限制的距離
        {
            if (transform.position.x < limitL)
            {
                transform.position = new Vector3(limitL, transform.position.y);
            }
            if (transform.position.x > limitR)
            {
                transform.position = new Vector3(limitR, transform.position.y);
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

    //後來使用另一種輸入input的方法
    /*public void Move(InputAction.CallbackContext ctx)
    {
        moveDir = ctx.ReadValue<Vector2>(); //讀取輸入值
        Debug.Log(moveDir);
    }*/ 
}
