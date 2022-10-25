using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRayManager : MonoBehaviour
{
    //main camera
    private Camera cam;
    [SerializeField]
    private GameObject prefabsPenalty;
    //score value
    private int scoreValue;
    //number of tolerated error before game over
    [SerializeField]
    private int errorCompt = 3;
    //number of hit remain before win
    [SerializeField]
    private int remainHit = 20;
    // score variable
    private int scoreToAdd = 20;
    private int scoreToRemove = 20;
    //bool indicate if the game is over
    private bool gameOver = false;
    // colors 
    [SerializeField]
    private Color penaltyColor, winColor, mainColor;
    //UI related
    [SerializeField]
    private GameOverScreenController gameOverScreen;
    [SerializeField]
    private WinScreenController winScreen;
    private UiTextController score, remain;
    //FX sound 
    [SerializeField]
    private AudioClip penaltySound, hitSound, gameOverSound, winSound;
    private AudioSource playerSound;
    private float normalFactor=0.1f;

    void Start()
    {
        score = GameObject.Find("ScoreCanva").GetComponent<UiTextController>();
        remain = GameObject.Find("RemainCanva").GetComponent<UiTextController>();
        playerSound = GameObject.Find("Player").GetComponent<AudioSource>();
        cam = Camera.main;
    }
    void Update()
    {
        //if game is over, the player cannot create ray anymore, win or loose
        if (!gameOver)
        {
            //if left mouse button is clicked, a ray is created from the screen point to the world 
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    //if the ray touch a object with the tag "target", score increase, a penalty sphere is created on the target;
                    if (hit.transform.CompareTag("Target"))
                    {
                        Debug.Log("World Space:" + hit.point + "/ permited error left : " + errorCompt + " | traget space : x:" + hit.transform.InverseTransformPoint(hit.point + hit.normal * 0.1f));

                        createPenatySphereOnHitPoint(hit);
                        addScore(scoreToAdd);
                        reduceRemain();
                        //play a song if hit remain, else display win screen
                        if (remainHit > 0)
                        {
                            playerSound.PlayOneShot(hitSound, 1.0f);
                        }
                        else
                        {
                            displayWin();

                        }
                    }
                    //else if the ray touch an object with the tag penalty, errorCompt variable decrease, if this variable =0, the player loose 
                    else if (hit.transform.CompareTag("Penalty"))
                    {
                        addScore(-scoreToRemove);
                        errorCompt--;
                        Debug.Log("penalty object / World Space:" + hit.point + "/ permited error left : " + errorCompt + " | traget space : x:" + hit.transform.InverseTransformPoint(hit.point + hit.normal * 0.1f));
                        //play a song and the background become red if the playre has permited error left, else display loose screen
                        if (errorCompt > 0)
                        {
                            playerSound.PlayOneShot(penaltySound, 1.0f);
                            StartCoroutine(changeColorTemporarly(0.025f, penaltyColor));
                        }
                        else
                        {
                            displayLoose();
                        }
                    }

                }

            }
        }

    }
    //instanciate a penaltySphere and give the information of the ray hit to the instancied object with setRayCastHit(); 
    void createPenatySphereOnHitPoint(RaycastHit hit)
    {
        GameObject penaltyObject = Instantiate(prefabsPenalty, hit.point + hit.normal * normalFactor, Quaternion.identity);
        penaltyObject.GetComponent<PenaltySphere>().setRaycastHit(hit);

    }

    //display loose(game over) screen
    void displayLoose()
    {
        gameOver = true;
        Debug.Log("you lose !");
        gameOverScreen.setup();
        remain.setVisible(false);
        cam.backgroundColor = penaltyColor;
        playerSound.PlayOneShot(gameOverSound, 1.0f);

    }
    //display win screen
    void displayWin()
    {
        gameOver = true;
        Debug.Log("you win !");
        winScreen.setup();
        remain.setVisible(false);
        cam.backgroundColor = winColor;
        playerSound.PlayOneShot(winSound, 1.0f);
    }
    void addScore(int value)
    {
        scoreValue += value;
        score.updateScore(scoreValue);
    }
    //reduce remain hit to do to win 
    void reduceRemain()
    {
        remainHit--;
        remain.updateRemain(remainHit);
    }

    public bool getGameOver()
    {
        return gameOver;
    }

    //change screen color red Temporarly
    IEnumerator changeColorTemporarly(float time, Color color)
    {
        cam.backgroundColor = color;
        yield return new WaitForSeconds(time);
        cam.backgroundColor = mainColor;
    }
}