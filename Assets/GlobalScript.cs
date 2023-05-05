using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalScript : MonoBehaviour
{
    public int score;
    public int health;
    public int pointThreshold;
    public float timeLeft;

    public float fireflyTimer;
    public bool hasPowerUp; 

    private static GameManagers gameManagers;
    public GameObject PowerUpPanel;

    public GameObject debrisManagerObj;
    public GameObject starManagerObj;
    public GameObject fireFlyObj;

    public bool stopGame;
    public bool startedGame; 

    // Start is called before the first frame update
    public void Start()
    {
        score = 0;
        health = 100;
        pointThreshold = 100;
        stopGame = false;

        // powerup 
        fireflyTimer = 5;
        hasPowerUp = false; 

        // timeLeft = 120; 
        timeLeft = 90;
        
        if (gameManagers == null)
        {
            gameManagers = FindObjectOfType<GameManagers>();
        }

        // only "start" game after button pressed 
        PauseGame();

        // enabled powerup panel for testing 
        PowerUpPanel.SetActive(false); 
    }


    // Update is called once per frame
    void Update()
    {
        if (hasWon())
        {
            Debug.Log("win :) ");
            gameManagers.WinGame();
        }

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

        // decrease firefly timer 
        if (hasPowerUp) {
            
            if (fireflyTimer > 0)
            {
                fireflyTimer -= Time.deltaTime;
            }
            else {
                // reset powerup if timer is up 
                PowerUpDeactivated(); 
            }
        }

        // decrease time left 
        // if game started 
        if (startedGame) {
            timeLeft -= Time.deltaTime;
        }
        

        // Debug.Log("time left: " + timeLeft);
    }

    public void PowerUpActivated()
    {
        hasPowerUp = true;
        fireflyTimer = 10;
        PowerUpPanel.SetActive(true);
        debrisManagerObj.SetActive(false);

        // play sound 
        gameManagers.playFireflySound(); 
    }

    public void PowerUpDeactivated()
    {
        hasPowerUp = false;
        PowerUpPanel.SetActive(false);
        fireflyTimer = 5;
        debrisManagerObj.SetActive(true);
    }

    public bool hasWon()
    {
        return (score >= pointThreshold); 
    }

    public void UpdateScore(int pointValue)
    {
        score += pointValue;
        if (score < 0)
        {
            score = 0;
        }
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
        // Time.timeScale = 0;
        stopGame = true;
        startedGame = false;

        // get debris manager - pause 
        debrisManagerObj.SetActive(false);

        // get star manager - pause 
        starManagerObj.SetActive(false);

        // get fire fly - pause 
        fireFlyObj.SetActive(false);
    }
    public void RestartGame()
    {
        // Time.timeScale = 1;
     
        // resetting 
        score = 0;
        health = 100;
        pointThreshold = 100;
        timeLeft = 90;
        stopGame = false;

        // get debris manager - resume 
        debrisManagerObj.SetActive(true);
        
        // get star manager - resume 
        starManagerObj.SetActive(true);

        fireFlyObj.SetActive(true);

        startedGame = true; 
    }
}
