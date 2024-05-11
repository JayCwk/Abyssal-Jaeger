using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameManger : MonoBehaviour
{
    public static GameManger instance;

    public int score = 0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bonusPointsText;
    public GameObject congratulationPanel;
    public int bonusPoints = 10;
    public float bonusPointsDisplayDuration = 2f; // Duration in seconds to display the bonus points

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

       
    }

    // Other methods...

    public void UpdateScore(int points)
    {
        score += points;

        // Check if the score is a multiple of 50
        if (score % 50 == 0)
        {
            // Award bonus points
            score += bonusPoints;

        }
        // Check if the score reaches 1000
        if (score >= 1000)
        {
            // Award 1 CC bonus
            CurrecnyManager.instance.AddCryptocurrency(1);
        }


        UpdateScoreUI();
    }
    
    private void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score.ToString();
    }

  
}
