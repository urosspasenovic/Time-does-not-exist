using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIHandler : MonoBehaviour
{
    [SerializeField]
    Canvas startingUI;
    [SerializeField]
    Canvas gameOverUI;
    [SerializeField]
    Canvas wonUI;
    [SerializeField]
    TMP_Text timeLabel;


    int timeCollected = 0;

    public void StartGame()
    {
        InputHandler.Instance.StartGame();
        startingUI.gameObject.SetActive(false);
    }


    public void ChangeTimeCollected()
    {
        timeCollected++;
        timeLabel.text = "Pieces of time collected: " + timeCollected + "/12";
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void GameLost()
    {
        gameOverUI.gameObject.SetActive(true);
    }
    public void GameWon()
    {
        wonUI.gameObject.SetActive(true);
    }
    public void Pause()
    {
        startingUI.gameObject.SetActive(true);
    }
    public void TryAgain()
    {
        SceneManager.LoadScene(0);
    }
}
