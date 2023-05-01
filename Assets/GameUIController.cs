using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUIController : MonoBehaviour
{
    public Slider healthBar;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText; 

/*    private float startTime;
    private float currentTime;*/

    private float timeLeft; 

    // Initialize with some starting health value and start the timer
    void Start()
    {
        // SetHealth(100);
        // startTime = Time.time;

        timeLeft = 120; 
    }

    void Update()
    {
        UpdateTimer();
        UpdateScore();
        UpdateHealth(); 
    }

    public void UpdateHealth()
    {
        // healthBar.value = health;

        GameObject obj = GameObject.Find("Global");
        GlobalScript g = obj.GetComponent<GlobalScript>();
        healthBar.value = g.health; 

    }

    private void UpdateTimer()
    {
        // currentTime = Time.time - startTime;
        
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0) timeLeft = 0;

        int minutes = (int)(timeLeft / 60);
        int seconds = (int)(timeLeft % 60);

        if (timeLeft <= 10)
        {
            timerText.color = Color.red;
        }
        else {
            timerText.color = Color.white; 
        }

        timerText.text = $"{minutes:00}:{seconds:00}";

    }

    private void UpdateScore()
    {
        GameObject obj = GameObject.Find("Global");
        GlobalScript g = obj.GetComponent<GlobalScript>();
        scoreText.color = Color.white;
        scoreText.text = "Score: " + g.score.ToString(); 
    }
}