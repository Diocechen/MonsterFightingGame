using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleWinLose : MonoBehaviour
{
    [SerializeField] private Text winLoseText;
    [SerializeField] private GameObject winLoseUI;

    public void Win()
    {
        winLoseUI.SetActive(true);
        winLoseText.text = "YOU WIN";
        Time.timeScale = 0f;
    }

    public void Lose()
    {
        winLoseUI.SetActive(true);
        winLoseText.text = "YOU LOSE";
        Time.timeScale = 0f;
    }
}
