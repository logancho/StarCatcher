using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagers : MonoBehaviour
{
    public GameObject startGamePanel;
    // Start is called before the first frame update
    public void StartGame()
    {
        startGamePanel.SetActive(false);
    }

}
