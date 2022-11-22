using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats : MonoBehaviour
{

    public static int Score;
    public int startScore;

    public static int Lives;
    public int startLives = 10;

    // Start is called before the first frame update
    void Start()
    {
        Score = startScore;
        Lives = startLives;
    }

    // Update is called once per frame
    void Update() 
    {

    }
}
