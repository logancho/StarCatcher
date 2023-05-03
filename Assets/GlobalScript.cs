using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalScript : MonoBehaviour
{
    public int score;
    public int health;
    public int pointThreshold;
    public float timeLeft; 
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        health = 100;
        pointThreshold = 100;
        timeLeft = 120; 
    }

    // Update is called once per frame
    void Update()
    {
        // tiems up 
        if (timeLeft == 0) {
            if (hasWon()) 
            {
                // call win scene / UI 
                Debug.Log("win :) ");
            } else
            {
                // call lose scene / UI 
                Debug.Log("lose :( "); 
            }
        }
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
}
