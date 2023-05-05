using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Valve.VR.Extras;

public class GameManagers : MonoBehaviour
{
    public GameObject startGamePanel;
    public GameObject winGamePanel;
    public GameObject loseGamePanel;
    public GameObject leftHand;
    public GameObject rightHand;
    public GameObject PowerUpPanel;

    // Start is called before the first frame update
    void Start()
    {
        startGamePanel.SetActive(true);
        winGamePanel.SetActive(false) ;
        loseGamePanel.SetActive(false);
        PowerUpPanel.SetActive(false);
    }
    
    public void StartGame()
    {
        startGamePanel.SetActive(false);
        winGamePanel.SetActive(false);
        loseGamePanel.SetActive(false);
        PowerUpPanel.SetActive(false);

        // resume game 
        GameObject obj = GameObject.Find("Global");
        GlobalScript g = obj.GetComponent<GlobalScript>();
        g.Start();
        g.RestartGame();

        // testing 
        // g.PowerUpActivated(); 
    }

    public void WinGame()
    {
        winGamePanel.SetActive(true);
        startGamePanel.SetActive(false);
        loseGamePanel.SetActive(false);
        PowerUpPanel.SetActive(false);
        DisplayLasers();

    }
    
    public void LoseGame()
    {
        loseGamePanel.SetActive(true);
        startGamePanel.SetActive(false);
        winGamePanel.SetActive(false);
        PowerUpPanel.SetActive(false);
        DisplayLasers();
    }

    public void DisplayLasers()
    {
        leftHand.GetComponent<SteamVR_LaserPointer>().pointer.GetComponent<MeshRenderer>().enabled = true;
        rightHand.GetComponent<SteamVR_LaserPointer>().pointer.GetComponent<MeshRenderer>().enabled = true;
    }
}
