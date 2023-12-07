using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleGamePlayUI : MonoBehaviour
{
    [SerializeField] private Image gameProgressBar;
    [SerializeField] private Image healthBar;
    [SerializeField] private Image skillPointBar;

    [SerializeField] private DamageableCharacter player;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private HandleSpawnEnemy spawnEnemy;

    private void Update()
    {
        SetSkillPointBar(gameInput.GetSkillPointAmountNormalize());
        SetPlayerHealthBar(player.GetHealthAmountNormalize());
        SetGameProgress(spawnEnemy.GetSpawnTimerNormalize());
    }

    public void SetGameProgress(float gameProgressNormalize)
    {
        gameProgressBar.fillAmount = gameProgressNormalize;
    }

    public void SetPlayerHealthBar(float playerHealthNormalize)
    {
        healthBar.fillAmount = playerHealthNormalize;
    }
    public void SetSkillPointBar(float skillPointNormalize)
    {
        skillPointBar.fillAmount = skillPointNormalize;
    }
}
