using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUIController : MonoBehaviour
{
    public Slider healthBar;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText; 

     
    void Start()
    {
 
    }

    void Update()
    {
        UpdateTimer();
        UpdateScore();
        UpdateHealth(); 
    }

    public void UpdateHealth()
    { 
        GameObject obj = GameObject.Find("Global");
        GlobalScript g = obj.GetComponent<GlobalScript>();
        healthBar.value = g.health; 

    }

    private void UpdateTimer()
    {
        GameObject obj = GameObject.Find("Global");
        GlobalScript g = obj.GetComponent<GlobalScript>();

        if (g.timeLeft < 0) g.timeLeft = 0;

        int minutes = (int)(g.timeLeft / 60);
        int seconds = (int)(g.timeLeft % 60);

        if (g.timeLeft <= 10)
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