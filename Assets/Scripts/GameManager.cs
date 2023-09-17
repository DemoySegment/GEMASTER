using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    private int score = 0;
    public TextMeshProUGUI scoreUI;
    void Start()
    {
        scoreUI.text = "Score: 0";
    }

    public void addScore(){
        score+= 10;
        scoreUI.text = "Score: " + score;
    }
}
