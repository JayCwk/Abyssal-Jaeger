using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ConfirmAndReturn : MonoBehaviour
{
    public void ReturnHomePage()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
