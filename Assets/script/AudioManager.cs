using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource MusicSource;
    [SerializeField] private AudioSource SfxSource;

    [SerializeField] public AudioClip Background;
    [SerializeField] public AudioClip shoot;
    [SerializeField] public AudioClip PlayeronHit;
    [SerializeField] public AudioClip EnemyonHit;
    [SerializeField] public AudioClip Playerdeath;
    [SerializeField] public AudioClip EnemyDeath;
    [SerializeField] public AudioClip PowerUp;
    [SerializeField] public AudioClip Pause;
    [SerializeField] public AudioClip OnClicked;
    [SerializeField] public AudioClip Cancel;

    private void Start()
    {
        MusicSource.clip = Background;
        MusicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SfxSource.PlayOneShot(clip);
    }
}
