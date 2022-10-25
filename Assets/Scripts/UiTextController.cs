using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiTextController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Text uiText;

    public void setVisible(bool t)
    {
        gameObject.SetActive(t);
    }
    // Update is called once per frame
    public void updateScore(int score)
    {
        uiText.text = score.ToString();
    }
    public void updateRemain(int remain)
    {
        uiText.text = "remain : " + remain;
    }
    public Text getText()
    {
        return uiText;
    }
    void Update()
    {

    }
}
