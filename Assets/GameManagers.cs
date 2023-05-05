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
    public GameObject tutorialPanel; 
    public GameObject leftHand;
    public GameObject rightHand;
    public GameObject PowerUpPanel;

    // for handling sound 
    public AudioSource buttonAudioSource;
    public AudioSource fireflyAudioSource;
    public AudioSource bgAudioSource;
    public AudioSource loseAudioSource;
    public AudioSource winAudioSource;

/*    public AudioClip gameBgMusic;
    public AudioClip winBgMusic;
    public AudioClip loseBgMusic;*/

    private bool playingRegMusic; 

    // Start is called before the first frame update
    void Start()
    {
        startGamePanel.SetActive(true);
        winGamePanel.SetActive(false) ;
        loseGamePanel.SetActive(false);
        PowerUpPanel.SetActive(false);
        tutorialPanel.SetActive(false);

        playRegMusic();
    }

    public void startTutorial()
    {
        winGamePanel.SetActive(false);
        startGamePanel.SetActive(false);
        loseGamePanel.SetActive(false);
        PowerUpPanel.SetActive(false);
        tutorialPanel.SetActive(true);
        //DisplayLasers();
    }

    public void playFireflySound()
    {
        fireflyAudioSource.Play(); 
    }

    public void playWinMusic()
    {
        bgAudioSource.Stop();
        winAudioSource.Play();
        loseAudioSource.Stop();
        playingRegMusic = false;
    }

    public void playLoseMusic()
    {
        bgAudioSource.Stop();
        winAudioSource.Stop();
        loseAudioSource.Play();
        playingRegMusic = false;
    }

    public void playRegMusic()
    {
        bgAudioSource.Play();
        winAudioSource.Stop();
        loseAudioSource.Stop();
        playingRegMusic = true;
    }

    public void playButtonSound()
    {
        buttonAudioSource.Play(); 
    }

    public void StartGame()
    {
        startGamePanel.SetActive(false);
        winGamePanel.SetActive(false);
        loseGamePanel.SetActive(false);
        PowerUpPanel.SetActive(false);
        tutorialPanel.SetActive(false);

        HideLasers();

        if (!playingRegMusic)
        {
            playRegMusic();
        }

        // resume game 
        GameObject obj = GameObject.Find("Global");
        GlobalScript g = obj.GetComponent<GlobalScript>();
        g.Start();
        g.RestartGame();

       

    }

    public void WinGame()
    {
        winGamePanel.SetActive(true);
        startGamePanel.SetActive(false);
        loseGamePanel.SetActive(false);
        PowerUpPanel.SetActive(false);
        tutorialPanel.SetActive(false);
        DisplayLasers();
        
        // playWinMusic();

        

    }
    
    public void LoseGame()
    {
        loseGamePanel.SetActive(true);
        startGamePanel.SetActive(false);
        winGamePanel.SetActive(false);
        PowerUpPanel.SetActive(false);
        tutorialPanel.SetActive(false);
        DisplayLasers();

        // playLoseMusic();
    }

    public void DisplayLasers()
    {
        leftHand.GetComponent<SteamVR_LaserPointer>().pointer.GetComponent<MeshRenderer>().enabled = true;
        rightHand.GetComponent<SteamVR_LaserPointer>().pointer.GetComponent<MeshRenderer>().enabled = true;
    }

    public void HideLasers()
    {
        leftHand.GetComponent<SteamVR_LaserPointer>().pointer.GetComponent<MeshRenderer>().enabled = false;
        rightHand.GetComponent<SteamVR_LaserPointer>().pointer.GetComponent<MeshRenderer>().enabled = false;
    }
}
