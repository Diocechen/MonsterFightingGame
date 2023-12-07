using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleHealthBarScale : MonoBehaviour
{
    //�@��scale���������D
    void Update()
    {
        DamageableCharacter damageableCharacter = GetComponentInParent<DamageableCharacter>();
        if (damageableCharacter)
        {
            if (damageableCharacter.transform.localScale.x > 0)
            {
                transform.localScale = new Vector3(0.02f, 0.02f, 0.02f);
            }
            else
            {
                transform.localScale = new Vector3(-0.02f, 0.02f, 0.02f);
            }
        }
    }
}
