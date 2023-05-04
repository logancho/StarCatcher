using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalScript : MonoBehaviour
{
    public int score;
    public int health;
    public int pointThreshold;
    public float timeLeft;

    private static GameManagers gameManagers;

    public GameObject debrisManagerObj;
    public GameObject starManagerObj; 

    public bool stopGame; 

    // Start is called before the first frame update
    public void Start()
    {
        score = 0;
        health = 100;
        pointThreshold = 100;
        timeLeft = 10;
        stopGame = false; 
        // timeLeft = 120; 

        if (gameManagers == null)
        {
            gameManagers = FindObjectOfType<GameManagers>();
        }

        // only "start" game after button pressed 
        PauseGame(); 
    }

    // Update is called once per frame
    void Update()
    {
        // times up 
        if (timeLeft <= 0) {
            // pause game 
            PauseGame(); 
            
            Debug.Log("times up"); 

            if (hasWon()) 
            {
                // call win scene / UI 
                Debug.Log("win :) ");
                gameManagers.WinGame(); 
            } else
            {
                // call lose scene / UI 
                Debug.Log("lose :( ");
                gameManagers.LoseGame();
            }
        }

        // decrease time left 
        timeLeft -= Time.deltaTime;
        // Debug.Log("time left: " + timeLeft);
    }

    public bool hasWon()
    {
        return (score >= pointThreshold); 
    }

    public void UpdateScore(int pointValue)
    {
        score += pointValue;
        // Debug.Log("score: " + score); 
    }

    public void DecreaseHealth()
    {
        if (health > 0) {
            health -= 10;
        }
   
        Debug.Log("health: " + health); 
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        stopGame = true;

        // get debris manager - puase 
        debrisManagerObj.SetActive(false);

        // get star manager - pause 
        starManagerObj.SetActive(false);
   
    }
    public void RestartGame()
    {
        Time.timeScale = 1;
     
        // resetting 
        score = 0;
        health = 100;
        pointThreshold = 100;
        timeLeft = 10;
        stopGame = false;

        // get debris manager - resume 
        debrisManagerObj.SetActive(true);
        
        // get star manager - resume 
        starManagerObj.SetActive(true);

    }
}
