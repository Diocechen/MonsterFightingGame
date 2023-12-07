using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameInput : MonoBehaviour
{
    public event EventHandler<AttackTypes> OnAttackAction;
    private PlayerControllInputAction inputActions;

    [SerializeField] private DeathHandleSystem _deathHandleSystem;

    [SerializeField] private DamageableCharacter damageableCharacter;

    [SerializeField] private CinemachineAnimator cinemachineAnimator;

    [SerializeField] private float[] AttackRate = new float[0];
    private float nextAttackTime = 0f;

    [SerializeField] private float skillPointRequire;
    [SerializeField] private float currentSkillPoint;

    //----
    [SerializeField] private HandlePauseUI UI;

    private void Awake()
    {
        inputActions = new PlayerControllInputAction();
        inputActions.PlayerControll.Enable();

        //官方的輸入系統
        inputActions.PlayerControll.Attack.performed += Attack_performed; //這是一個Action
        inputActions.PlayerControll.Pause.performed += Pause_performed;

        _deathHandleSystem.DeathHappen += AddCurrentSkillPoint;
    }

    //基本上是從input action的輸入 -> 執行 OnAttackAction
    private void Attack_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        /*if (OnAttackAction != null)
        {
            OnAttackAction(this, EventArgs.Empty);
        }*/
        //Same
        //OnAttackAction?.Invoke(this, EventArgs.Empty);

        if (!damageableCharacter.IsHurt() && !damageableCharacter.IsDead())
        {
            //不同鍵盤輸入會有不同攻擊
            if (Time.time >= nextAttackTime) //如果當前時間經過了攻擊的間隔才能攻擊
            {
                if (Keyboard.current[Key.Numpad1].wasPressedThisFrame)
                {
                    //Debug.Log(1);
                    OnAttackAction?.Invoke(this, new AttackTypes(1));
                    nextAttackTime = Time.time + (1.0f / AttackRate[0]);
                }
                else if (Keyboard.current[Key.Numpad2].wasPressedThisFrame)
                {
                    //Debug.Log(2);
                    OnAttackAction?.Invoke(this, new AttackTypes(2));
                    nextAttackTime = Time.time + (1.0f / AttackRate[1]);
                }
                else if (Keyboard.current[Key.Numpad3].wasPressedThisFrame)
                {
                    if (currentSkillPoint >= skillPointRequire)
                    {
                        cinemachineAnimator.CameraCloseUp();
                        TimeManager.Instance.SlowMotion();
                        damageableCharacter.Invincible(true);
                        OnAttackAction?.Invoke(this, new AttackTypes(3));
                        nextAttackTime = Time.time + (1.0f / AttackRate[2]);
                        currentSkillPoint = 0f;
                    }
                }
            }
        }
        
    }
    private void Pause_performed(InputAction.CallbackContext obj)
    {
        if (!UI.GetPause())
        {
            UI.Pause();
        }
        else
        {
            UI.Resume();
        }
    }

    public float GetSkillPointAmountNormalize()
    {
        return currentSkillPoint / skillPointRequire;
    }
    public void AddCurrentSkillPoint(object sender, SkillPointArgs e)
    {
        if (currentSkillPoint < skillPointRequire)
        {
            currentSkillPoint += e.addSkillPoint;
        }
        if (currentSkillPoint >= skillPointRequire)
        {
            currentSkillPoint = skillPointRequire;
        }
    }

    public Vector2 GetMovementVector2D()
    {
        Vector2 inputVector = inputActions.PlayerControll.Move.ReadValue<Vector2>(); //讀取輸入值

        return inputVector;
    }
}

public class AttackTypes : System.EventArgs
{
    public int inputType;
    public AttackTypes(int type)
    {
        inputType = type;
    }
}
