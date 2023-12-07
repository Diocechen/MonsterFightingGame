using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleHint : MonoBehaviour
{
    [SerializeField] private SpriteRenderer hint;
    [SerializeField] private GameInput gameInput;
    
    void Update()
    {
        if (gameInput.GetSkillPointAmountNormalize() == 1)
        {
            hint.enabled = true;
        }
        else
        {
            hint.enabled = false;
        }
    }
}
