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

    // Start is called before the first frame update
    public void Start()
    {
        score = 0;
        health = 100;
        pointThreshold = 100;
        stopGame = false;

        // powerup 
        fireflyTimer = 10;
        hasPowerUp = false; 

        // timeLeft = 120; 
        timeLeft = 20;
        
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
        timeLeft -= Time.deltaTime;

        // Debug.Log("time left: " + timeLeft);
    }

    public void PowerUpActivated()
    {
        hasPowerUp = true;
        fireflyTimer = 10;
        PowerUpPanel.SetActive(true);
        debrisManagerObj.SetActive(false);
    }

    public void PowerUpDeactivated()
    {
        hasPowerUp = false;
        PowerUpPanel.SetActive(false);
        fireflyTimer = 10;
        debrisManagerObj.SetActive(true);
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

        fireFlyObj.SetActive(false);
    }
    public void RestartGame()
    {
        Time.timeScale = 1;
     
        // resetting 
        score = 0;
        health = 100;
        pointThreshold = 100;
        timeLeft = 20;
        stopGame = false;

        // get debris manager - resume 
        debrisManagerObj.SetActive(true);
        
        // get star manager - resume 
        starManagerObj.SetActive(true);

        fireFlyObj.SetActive(true);
    }
}
