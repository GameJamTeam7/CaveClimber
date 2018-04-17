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

        [SerializeField] [Tooltip("How Quickly Buttons Are Spawned")]
        private float buttonSpawnRate;

        [Tooltip("How Often The Game Incresses Difficulty")] [SerializeField]
        private float increaseInterval;

        [SerializeField] [Tooltip("Starting Speed")]
        private float startingSpeed;

        [SerializeField] [Tooltip("How Much Score Is Incressed By When A Button Is Hit")]
        private float scoreToAdd;

        [SerializeField]
        private GameObject Player;

        [SerializeField]
        private float playerClimbAmount;

        [SerializeField]
        private float playerFallDisance;

        [SerializeField]
        List<AudioClip> audioClips = new List<AudioClip>();


        #endregion

        #region private variables
        private float gameTimer;
        private float difficultyTimer;
        private float difficulty;
        private float gameSpeed;
        private Text scoreText;
        private float score;
        private int Health;
        private float MoveSpeed;
        private Vector3 PlayerStartPos;
        private Camera cam;
        private bool firstLoop;
        private SaveState s;
        private AudioSource audioSource;

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
                return buttonSpawnRate;
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
            //currentGameState = GameState.GameState;

            cam = Camera.main;

            s = new SaveState();

            s.read();

            mainMenu = GameObject.FindGameObjectWithTag("MainMenu_Canvas").GetComponent<Canvas>();
            gamePlay = GameObject.FindGameObjectWithTag("Gameplay_Canvas").GetComponent<Canvas>();
            endGame = GameObject.FindGameObjectWithTag("EndGame_Canvas").GetComponent<Canvas>();
            pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu_Canvas").GetComponent<Canvas>();
            credits = GameObject.FindGameObjectWithTag("Credits_Canvas").GetComponent<Canvas>();


            foreach(var i in gameObject.GetComponents<AudioSource>())
            {
                if (i.clip == null)
                {
                    audioSource = i;
                }
            }

            firstLoop = true;


            gameSpeed = startingSpeed;
            Health = 3;
            MoveSpeed = 0.0005f;
            PlayerStartPos = Player.transform.position;
            scoreText = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
        }

        //automaticaly destorys all new variables on programm shut down
        ~GameManager() { }

        void Update()
        {
            if (currentGameState == GameState.MenuState)
            {
                if (firstLoop)
                {
                    DisableOtherCanvases(mainMenu);
                    firstLoop = false;
                    mainMenu.enabled = true;
                    foreach (GameObject i in GameObject.FindGameObjectsWithTag("HightScoreTexts"))
                    {
                        i.GetComponent<Text>().text = ((int)s.SavedScore).ToString();
                    }
                        
                        
                        
                    //    = ((int)s.SavedScore).ToString();
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

                        difficulty = (Mathf.Pow(gameSpeed, difficultyCurve) / 100);
                        gameSpeed += difficulty;
                        buttonSpawnRate -= difficulty;
                        //  Debug.Log(bpm);
                    
                }
                scoreText.text = ((int)score).ToString();

                //PlayerMove
                //if (Player.transform.position.magnitude - PlayerStartPos.magnitude < 0.09)
                //{
                //    Player.transform.Translate(Vector3.up * MoveSpeed);
                //}
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
                    foreach (GameObject i in GameObject.FindGameObjectsWithTag("HightScoreTexts"))
                    {
                        i.GetComponent<Text>().text = ((int)s.SavedScore).ToString();
                    }
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
            audioSource.clip = audioClips[0];
            audioSource.Play();
            score = score + scoreToAdd;
            if (Player.transform.position.y < (cam.transform.position.y + (0.5 * cam.orthographicSize)))
            {
                Player.transform.Translate(Vector3.up * playerClimbAmount);
            }
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
            audioSource.clip = audioClips[1];
            audioSource.Play();
            Debug.Log("damage Taken:" + --Health);
            Player.transform.Translate(0, -playerFallDisance, 0);
            //GameOver
            if (Player.transform.position.y < (cam.transform.position.y - (0.5 * cam.orthographicSize)))
            {
                Die();
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
            if (currentGameState == GameState.MenuState)
            {
                currentGameState = GameState.CreditsState;
                firstLoop = true;
            }
            else if(currentGameState == GameState.CreditsState)
            {
                currentGameState = GameState.MenuState;
                firstLoop = true;
            }
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

        private void Die()
        {

                currentGameState = GameState.EndState;
                firstLoop = true;

                //check if we have a high score
                s.read();

                //Compare this games score to the score in the file IF yes overwrite old score
                if (score > s.SavedScore)
                {
                    s.SavedScore = score;
                    s.write();
                }
            
        }

    }
}


