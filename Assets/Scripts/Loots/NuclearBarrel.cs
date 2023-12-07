using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NuclearBarrel : MonoBehaviour
{
    [SerializeField] private int healAmount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.TryGetComponent<DamageableCharacter>(out var damageableCharacter))
            {
                damageableCharacter.GetHeal(healAmount);
                Destroy(gameObject);
                HandleParticle.instance.SummonParticle(1, collision.transform.position);
            }
        }
    }
}
