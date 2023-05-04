using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PowerUpPanelController : MonoBehaviour
{

    public TextMeshProUGUI timerText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimer(); 
        
    }

    private void UpdateTimer()
    {
        GameObject obj = GameObject.Find("Global");
        GlobalScript g = obj.GetComponent<GlobalScript>();

        if (g.fireflyTimer < 0) g.fireflyTimer = 0;

        int minutes = (int)(g.fireflyTimer / 60);
        int seconds = (int)(g.fireflyTimer % 60);

        if (g.fireflyTimer <= 3)
        {
            timerText.color = Color.red;
        }
        else
        {
            timerText.color = Color.white;
        }

        timerText.text = $"{minutes:00}:{seconds:00}";

    }
}
