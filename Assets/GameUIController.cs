using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUIController : MonoBehaviour
{
    public Slider healthBar;
    public TextMeshProUGUI timerText;

    private float startTime;
    private float currentTime;

    // Initialize with some starting health value and start the timer
    void Start()
    {
        SetHealth(100);
        startTime = Time.time;
    }

    void Update()
    {
        UpdateTimer();
    }

    public void SetHealth(float health)
    {
        healthBar.value = health;
    }

    private void UpdateTimer()
    {
        currentTime = Time.time - startTime;
        int minutes = (int)(currentTime / 60);
        int seconds = (int)(currentTime % 60);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }
}
