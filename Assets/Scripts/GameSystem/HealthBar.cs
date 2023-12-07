using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private const float BAR_FADE_TIMER_MAX = 0.5f;

    private Image barImage;
    private Image barFadeImage; //��q�����ܮĪG
    private Color barFadeColor;
    private float barFadeTimer = 0f;

    private void Awake()
    {
        barImage = transform.Find("Bar").GetComponent<Image>();
        
        barFadeImage = transform.Find("BarFade").GetComponent<Image>();
        barFadeColor = barFadeImage.color;
        barFadeColor.a = 0;
        barFadeImage.color = barFadeColor;
    }

    private void Update()
    {
        if (barFadeColor.a > 0) //�p�G���ܮĪG�i��
        {
            barFadeTimer -= Time.deltaTime;
            if (barFadeTimer < 0) //���ܮĪG�ܳz��
            {
                float fadeAmount = 5;
                barFadeColor.a -= fadeAmount * Time.deltaTime;
                barFadeImage.color = barFadeColor;
            }
        }
    }

    public void SetHealth(float healthAmountNormalized)
    {
        if (barFadeColor.a <= 0) //�ˬd���ܬO�_�w�g�i��
        {
            barFadeImage.fillAmount = barImage.fillAmount; //����e�����A
            //print(barFadeImage.fillAmount);
        }
        barFadeColor.a = 1; 
        barFadeImage.color = barFadeColor;
        barFadeTimer = BAR_FADE_TIMER_MAX;

        barImage.fillAmount = healthAmountNormalized;
    }
}
