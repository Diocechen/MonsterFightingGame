using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private const float BAR_FADE_TIMER_MAX = 0.5f;

    private Image barImage;
    private Image barFadeImage; //血量條漸變效果
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
        if (barFadeColor.a > 0) //如果漸變效果可見
        {
            barFadeTimer -= Time.deltaTime;
            if (barFadeTimer < 0) //漸變效果變透明
            {
                float fadeAmount = 5;
                barFadeColor.a -= fadeAmount * Time.deltaTime;
                barFadeImage.color = barFadeColor;
            }
        }
    }

    public void SetHealth(float healthAmountNormalized)
    {
        if (barFadeColor.a <= 0) //檢查漸變是否已經可見
        {
            barFadeImage.fillAmount = barImage.fillAmount; //扣血前的狀態
            //print(barFadeImage.fillAmount);
        }
        barFadeColor.a = 1; 
        barFadeImage.color = barFadeColor;
        barFadeTimer = BAR_FADE_TIMER_MAX;

        barImage.fillAmount = healthAmountNormalized;
    }
}
