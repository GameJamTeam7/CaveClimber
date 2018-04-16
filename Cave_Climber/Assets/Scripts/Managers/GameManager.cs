using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public enum GameStates
    {
        MenuState,
        GameState,
        PauseState,
        EndState
    };





    #region  exposed variables
    [Range(0.0001f, 2)]
    [SerializeField]
    [Tooltip("Changes How Hard The Game Is Each Incresses")]
    private float difficultyCurve;
    [Tooltip("How Often The Game Incresses Difficulty")]
    [SerializeField]
    private float increaseInterval;
    [SerializeField]
    private float maxButtonsPerSecond;
    [SerializeField]
    [Tooltip("Starting Speed")]
    private float startingSpeed;
    [SerializeField]
    [Tooltip("How Much Score Is Incressed By When A Button Is Hit")]
    private float scoreMultiplyer;
    #endregion

    #region private variables
    private float gameTimer;
    private float difficultyTimer;
    private float difficulty;
    private float buttonsPerSecond;
    private float gameSpeed;
    private Text scoreText;
    private float score;

    #endregion

    #region Gets and Sets
    public float GameSpeed
    {
        get
        {
            return gameSpeed;
        }
    }

    public float ButtonsPerSecond
    {
        get
        {
            if (buttonsPerSecond > maxButtonsPerSecond)
            {
                buttonsPerSecond = maxButtonsPerSecond;
            }
            return buttonsPerSecond;
        }
    }


    #endregion


    void Start()
    {
        buttonsPerSecond = maxButtonsPerSecond;
        gameSpeed = startingSpeed;

        scoreText = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
    }

    void Update()
    {
        difficultyTimer += Time.deltaTime;
        gameTimer += Time.deltaTime;
        score += Time.deltaTime;

        if (difficultyTimer >= increaseInterval)
        {
            difficultyTimer = 0.0f;
            if (gameSpeed < maxButtonsPerSecond)
            {
                difficulty = (Mathf.Pow(gameSpeed, difficultyCurve) / 100);
                gameSpeed += difficulty;
                buttonsPerSecond -= difficulty;
                //  Debug.Log(bpm);

            }
            else
            {
                gameSpeed = maxButtonsPerSecond;
            }
        }


        scoreText.text = ((int)score).ToString();
    }

    public void IncresseScore()
    {
        score = score * scoreMultiplyer;
    }

}


