using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//for now, win screen is the same that GameOverScreen, but in the future more feature can be add
public class WinScreenController : MonoBehaviour
{
    // Start is called before the first frame update
    public void setup(){
        gameObject.SetActive(true);
    }
    public void restartButton()
    {
        SceneManager.LoadScene("Game Scene");
    }
    public void mainMenuButton()
    {
        SceneManager.LoadScene("Main Menu");

    }
}
