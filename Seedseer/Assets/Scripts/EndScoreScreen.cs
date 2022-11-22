using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScoreScreen : MonoBehaviour
{
    public TextMeshProUGUI endScore;
    // Start is called before the first frame update
    void Start()
    {
        endScore.text = "Final score: " + GameStats.Score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
