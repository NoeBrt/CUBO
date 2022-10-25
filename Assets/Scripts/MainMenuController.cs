using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // Start is called before the first frame update
    //if exit button is pressed, quit the game
    public void exitButton()
    {
        Application.Quit();
        Debug.Log("Game Closed");
    }
    //if start game button is pressed, reload the game scene, that will reload the game;
    public void startGame()
    {
        SceneManager.LoadScene("Game Scene");
    }
}
