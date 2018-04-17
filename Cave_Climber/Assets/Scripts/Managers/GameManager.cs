using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Global
{
    public class GameManager : MonoBehaviour
    {

        public enum GameState
        {
            MenuState,
            GameState,
            PauseState,
            EndState,
            CreditsState
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
        private float scoreToAdd;
        [SerializeField]
        private GameObject Player;
        #endregion

        #region private variables
        private float gameTimer;
        private float difficultyTimer;
        private float difficulty;
        private float buttonsPerSecond;
        private float gameSpeed;
        private Text scoreText;
        private float score;
        private int Health;
        private float MoveSpeed;
        private Vector3 PlayerStartPos;

        private bool firstLoop;

        private Canvas currentCanvas;
        private Canvas mainMenu;
        private Canvas gamePlay;
        private Canvas endGame;
        private Canvas pauseMenu;
        private Canvas credits;

        private GameState currentGameState;

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
        public GameState CurrentGameState
        {
            get
            {
                return currentGameState;
            }
        }

        public float Score
        {
            get
            {
                return score;
            }
        }

        #endregion


        void Start()
        {
            currentGameState = GameState.MenuState;
            //Testing
            //   currentGameState = GameState.GameState;

            mainMenu = GameObject.FindGameObjectWithTag("MainMenu_Canvas").GetComponent<Canvas>();
            gamePlay = GameObject.FindGameObjectWithTag("Gameplay_Canvas").GetComponent<Canvas>();
            endGame = GameObject.FindGameObjectWithTag("EndGame_Canvas").GetComponent<Canvas>();
            pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu_Canvas").GetComponent<Canvas>();
            credits = GameObject.FindGameObjectWithTag("Credits_Canvas").GetComponent<Canvas>();

            currentCanvas = mainMenu;

            firstLoop = true;

            buttonsPerSecond = maxButtonsPerSecond;
            gameSpeed = startingSpeed;
            Health = 3;
            MoveSpeed = 0.0005f;
            PlayerStartPos = Player.transform.position;
            scoreText = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
        }

        void Update()
        {
            if (currentGameState == GameState.MenuState)
            {
                if (firstLoop)
                {
                    DisableOtherCanvases(mainMenu);
                    firstLoop = false;
                    mainMenu.enabled = true;
                }
            }

            else if (currentGameState == GameState.GameState)
            {
                if (firstLoop)
                {
                    DisableOtherCanvases(gamePlay);
                    firstLoop = false;
                }
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

                //PlayerMove
                if (Player.transform.position.magnitude - PlayerStartPos.magnitude < 0.09)
                {
                    Player.transform.Translate(Vector3.up * MoveSpeed);
                }
            }

            else if (currentGameState == GameState.PauseState)
            {
                if (firstLoop)
                {
                    DisableOtherCanvases(pauseMenu);
                    firstLoop = false;
                }
            }

            else if (currentGameState == GameState.EndState)
            {
                if (firstLoop)
                {
                    DisableOtherCanvases(endGame);
                    firstLoop = false;
                }
            }
            else if (currentGameState == GameState.CreditsState)
            {
                if(firstLoop)
                {
                    DisableOtherCanvases(credits);
                    firstLoop = false;
                }
            }

        }

        public void IncresseScore()
        {
            score = score + scoreToAdd;
        }

        public void PauseGame()
        {
            if (currentGameState == GameState.GameState)
            {
                currentGameState = GameState.PauseState;
                firstLoop = true;
            }
            else
            {
                currentGameState = GameState.GameState;
                firstLoop = true;
            }
        }

        public void TakeDamage()
        {
            Debug.Log("damage Taken:" + --Health);
            Player.transform.Translate(0, -0.5f, 0);
            //GameOver
            if (Health <= 0)
            {
                currentGameState = GameState.EndState;
                firstLoop = true;
            }
            foreach (var button in GameObject.FindGameObjectsWithTag("Button"))
            {
                Destroy(button);
            }
        }

        public void StartGame()
        {
            currentGameState = GameState.GameState;
            Player.transform.position = PlayerStartPos;
            firstLoop = true;
        }

        public void EndGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
        }

        public void OpenCanavs()
        {
            currentGameState = GameState.CreditsState;
            firstLoop = true;
        }

        private void DisableOtherCanvases(Canvas a_canvas)
        {
            a_canvas.enabled = true;
            if(a_canvas != mainMenu)
            {
                mainMenu.enabled = false;
            }
            if (a_canvas != gamePlay)
            {
                gamePlay.enabled = false;
            }
            if (a_canvas != endGame)
            {
                endGame.enabled = false;
            }
            if (a_canvas != pauseMenu)
            {
                pauseMenu.enabled = false;
            }
            if (a_canvas != credits)
            {
                credits.enabled = false;
            }
              
        }

    }
}


