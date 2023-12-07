using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleWaveHint : MonoBehaviour
{
    public static HandleWaveHint Instance;  

    [SerializeField] private Text waveHintText;

    private void Awake()
    {
        Instance = this;
    }

    public void SetHint(string s)
    {
        waveHintText.text = s;
    }

    public void HideHint() 
    {
        waveHintText.text = " ";
    }
}
