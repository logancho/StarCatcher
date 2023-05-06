using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JarUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    void Start()
    {
        UpdateScore();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScore();
    }

    private void UpdateScore()
    {
        GameObject obj = GameObject.Find("Global");
        GlobalScript g = obj.GetComponent<GlobalScript>();
        scoreText.color = Color.white;
        scoreText.text = "Star Points: " + g.score.ToString() + " / " + g.pointThreshold.ToString();
    }
}
