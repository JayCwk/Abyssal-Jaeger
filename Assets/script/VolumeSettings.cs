using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer myAudio;
    [SerializeField] private Slider mySlider1;
    [SerializeField] private Slider mySlider2;

    public void SetMusicVolume()
    {
        float volume = mySlider1.value;
        myAudio.SetFloat("Music", Mathf.Log10(volume)*20);
    }

    public void SetMusicVolume1()
    {
        float volume = mySlider2.value;
        myAudio.SetFloat("SFX", Mathf.Log10(volume) * 20);
    }
}
