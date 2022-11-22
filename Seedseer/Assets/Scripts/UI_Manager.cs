using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UI_Manager : MonoBehaviour
{

    public TextMeshProUGUI currentScore;
    public TextMeshProUGUI currentLives;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentLives.text = GameStats.Lives.ToString() + " LIVES LEFT";
        currentScore.text = "Score: " + GameStats.Score.ToString();
    }
}
