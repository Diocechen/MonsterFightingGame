using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

//可以掛在各種腳色上
public class DamageableCharacter : MonoBehaviour
{
    [SerializeField] DeathHandleSystem _deathHandleSystem;

    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;
    [SerializeField] private AnimatorScript animatorScript;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private GameObject floatingPoint;
    [SerializeField] private HandleWinLose winLose;

    private bool Dead = false;
    private bool Hurt = false;
    private bool Dizzy = false;
    private bool invincible = false;

    private void Awake()
    {
        //我覺得這不是一個好方法，但是我想不到更好的了。
        GameObject deathHandleSystemObj = GameObject.FindGameObjectsWithTag("DeathHandleSystem")[0];
        _deathHandleSystem = deathHandleSystemObj.GetComponent<DeathHandleSystem>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void Invincible(bool tf) 
    {
        invincible = tf;
    }

    public void OnKnockBack(Vector2 force)
    {
        Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.AddForce(force, ForceMode2D.Impulse);
    }

    public void GetDamage(int damageAmount)
    {
        if (!invincible)
        {
            if (currentHealth > 0)
            {
                Hurt = true;
                GameObject floaty = Instantiate(floatingPoint, transform.position, Quaternion.identity);
                floaty.transform.GetChild(0).GetComponent<TextMesh>().text = damageAmount.ToString();
                currentHealth -= damageAmount;
                if (healthBar)
                {
                    healthBar.SetHealth((float)currentHealth / (float)maxHealth);
                }
                animatorScript.ExecuteHurtAnimation();
            }

            if (currentHealth <= 0)
            {
                if (!Dead)
                {
                    currentHealth = 0;
                    Dead = true;
                    animatorScript.ExecuteDeadAnimation();
                }
            }
        }
    }

    public void GetHeal(int healAmount)
    {
        currentHealth += healAmount;
        
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void Death()
    {
        if (_deathHandleSystem)
        {
            //print("Death");
            if (gameObject.CompareTag("Enemy"))
            {
                _deathHandleSystem.AnEmemyDied(10f);
                Destroy(gameObject);
            }
        }

        if (gameObject.CompareTag("Player"))
        {
            winLose.Lose();
        }
    }

    public void OnDizzy()
    {
        Dizzy = true;
    }

    public void RecoverDizzy()
    {
        Dizzy = false;
    }

    public float GetHealthAmountNormalize()
    {
        return (float)currentHealth / (float)maxHealth;
    }

    public bool IsDizzy()
    {
        return Dizzy;
    }

    public void Recovery()
    {
        Hurt = false;
    }

    public bool IsDead()
    {
        return Dead;
    }

    public bool IsHurt()
    {
        return Hurt;
    }

}
