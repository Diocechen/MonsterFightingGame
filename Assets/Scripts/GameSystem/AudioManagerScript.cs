using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class AudioManagerScript : MonoBehaviour
{
    static public AudioManagerScript instance; //singleton
    private List<AudioSource> audios = new();
    /*
    0. music
    1. player attack
    2. player footstep
    3. enemy attack
    4. enemy footstep
    5. enemy get hit
     */
    [SerializeField] private AudioClip bgmBattle;
    [SerializeField] private AudioClip seFootStep1;
    [SerializeField] private AudioClip seFootStep2;
    [SerializeField] private AudioClip seFootStep3;
    [SerializeField] private AudioClip seFootStep4;
    [SerializeField] private AudioClip sePlayerAttack1;
    [SerializeField] private AudioClip sePlayerAttack2;
    [SerializeField] private AudioClip sePlayerSkill1;
    [SerializeField] private AudioClip sePlayerSkill2;
    [SerializeField] private AudioClip seEnemyAttack;
    [SerializeField] private AudioClip seEnemyGetHit;

    //------

    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SFXSlider;
    private void Awake()
    {
        instance = this;
        for (int i = 0; i < 6; i++)
        {
            var audio = this.gameObject.AddComponent<AudioSource>(); //加入通道
            audios.Add(audio); //將audio source加入list
        }

        PlayAudio(0, "bgmBattle", true);
    }

    public void PlayAudio(int index, string name, bool looping)
    {
        var clip = GetAudioClip(name);
        if (clip)
        {
            var audio = audios[index];
            audio.clip = clip;
            audio.loop = looping;
            audio.Play();
        }
    }

    public AudioClip GetAudioClip(string name)
    {
        switch (name)
        {
            case "bgmBattle":
                return bgmBattle;
            case "seFootStep1":
                return seFootStep1;
            case "seFootStep2":
                return seFootStep2;
            case "seFootStep3":
                return seFootStep3;
            case "seFootStep4":
                return seFootStep4;
            case "sePlayerAttack1":
                return sePlayerAttack1;
            case "sePlayerAttack2":
                return sePlayerAttack2;
            case "sePlayerSkill1":
                return sePlayerSkill1;
            case "sePlayerSkill2":
                return sePlayerSkill2;
            case "seEnemyAttack":
                return seEnemyAttack;
        }
        return null;
    }

    public void SetMusicVolume()
    {
        audios[0].volume = musicSlider.value;
    }

    public void SetEffectVolume()
    {
        audios[1].volume = SFXSlider.value;
        audios[2].volume = SFXSlider.value;
        audios[3].volume = SFXSlider.value;
        audios[4].volume = SFXSlider.value;
        audios[5].volume = SFXSlider.value;
    }
}
