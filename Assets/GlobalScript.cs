using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalScript : MonoBehaviour
{
    public int score;
    public int health; 
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        health = 100; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore()
    {
        score++;
        Debug.Log("score: " + score); 
    }

    public void DecreaseHealth()
    {
        if (health >= 0) {
            health -= 10;
        }
   
        Debug.Log("health: " + health); 
    }
}
