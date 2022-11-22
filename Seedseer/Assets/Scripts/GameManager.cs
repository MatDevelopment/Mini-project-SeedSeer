using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    bool gameHasEnded = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (gameHasEnded == true)       // If the game has ended, exit the Update so end game screen isn't loaded each frame
            return;

        if (GameStats.Lives <= 0)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        gameHasEnded = true;
        Debug.Log("Game Over");
        SceneManager.LoadScene("LossEndgameScene");
    }


}
