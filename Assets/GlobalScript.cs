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
        if (hasWon())
        {
            Debug.Log("You win! :) ");
        } else
        {
            if (timeLeft <= 0)
            {
                Debug.Log("lose :( ");
            }
            //Otherwise, you're still j playing
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
