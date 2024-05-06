using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class ConfirmAndReturn : MonoBehaviour
{
    AudioManager audiomg;

    private void Start()
    {
        audiomg = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    public void ReturnHomePage()
    {
       
        SceneManager.LoadSceneAsync(0);
    }
}
