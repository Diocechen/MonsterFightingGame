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
                moveDir = gameInput.GetMovementVector2D(); //�z�LgameInput��o��J��

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
        else //�i��|�]�X�@�I����Z��
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

    //��ӨϥΥt�@�ؿ�Jinput����k
    /*public void Move(InputAction.CallbackContext ctx)
    {
        moveDir = ctx.ReadValue<Vector2>(); //Ū����J��
        Debug.Log(moveDir);
    }*/ 
}
