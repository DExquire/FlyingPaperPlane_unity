using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject menuScreen;
    public GameObject loadingScreen;
    public GameObject gameScreen;
    public GameObject clickerLevel;
    public GameObject gameWinScreen;
    public GameObject gameOverScreen;
    public GameObject gameButtons;
    public Button tryAgainBtn;
    public BgController movingCity;
    public BgController movingRoad;
    public Transform bonusesObject;
    public BallBehaviour player;
    public Button playButton;
    public float scoreNum;
    public float scoreNumSpeed;
    public List<Text> scoreTxt;
    public List<GameObject> columns;
    public Game gameShop;
    public float lives;
    public float startLives;
    public Text livesText;
    public List<Text> coinsText;
    public int armorTime;
    public GameObject effectObject;
    public bool isMiniGameActive = false;
    public float miniGameRemainTime;
    public float miniGameLimitTime;
    public bool isPaused;
    public Text gameOverCoins;
    public Text gameOverScore;
    public int startCoins;
    public int coinsEarned;

    public float timerLightning;
    public float maxTimerLightning;
    public bool isLightningEffect;

    public LevelSpeed currentLevelSpeed;

    void Start()
    {
        foreach (GameObject column in columns)
        {
            column.SetActive(false);
        }
        menuScreen.SetActive(true);
        loadingScreen.SetActive(true);
        Invoke(nameof(CloseLoadingScreen), 3);
        gameScreen.SetActive(false);
        gameButtons.SetActive(false);
        playButton.onClick.AddListener(StartGame);
        gameShop.Coins = PlayerPrefs.GetInt("coins");
        SetCoinText();
        armorTime = PlayerPrefs.GetInt("armor");
        int armor = armorTime / 3;
        for (int i = 0; i < effectObject.transform.childCount; i++)
        {
            if(i+1 <= armor)
            {
                effectObject.transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                effectObject.transform.GetChild(i).gameObject.SetActive(false);
            }
            
        }
    }

   
    void Update()
    {
        if(!isPaused && !isMiniGameActive)
        {
            DifficultyLevel();
        }

        if(isLightningEffect && !isPaused && !isMiniGameActive)
        {
            LightningTimer();
        }
       
    }

    private void FixedUpdate()
    {
        if (gameScreen.activeSelf && !isMiniGameActive && !isPaused)
        {
            scoreNum += Time.fixedDeltaTime * scoreNumSpeed;
            scoreTxt[0].text = Mathf.FloorToInt(scoreNum).ToString();
        }
    }

    public void SetCoinText()
    {
        foreach(Text coin in coinsText)
        {
            coin.text = gameShop.Coins.ToString();
        }
    }

    public void CloseLoadingScreen()
    {
        loadingScreen.SetActive(false);
    }

    public void StartGame()
    {
        scoreNum = 0;
        /*      foreach(Text score in scoreTxt)
              {
                  score.text = scoreNum.ToString();
              }*/
        Time.timeScale = 1;
        menuScreen.SetActive(false);
        foreach (GameObject column in columns)
        {
            column.SetActive(true);
        }
        gameScreen.SetActive(true);
        gameButtons.SetActive(true);
        lives = startLives;
        livesText.text = lives.ToString();

        for (int i = 0; i < effectObject.transform.childCount; i++)
        {
            if (i < armorTime / 3)
                effectObject.transform.GetChild(i).gameObject.SetActive(true);
            else
                effectObject.transform.GetChild(i).gameObject.SetActive(false);
        }
        startCoins = gameShop.Coins;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1;
    }

    public void MiniGamePause()
    {
        //stop player
        player.speed = 0;
        //stop city
        movingCity.x = 0;
        //stop bonuses
        bonusesObject.gameObject.SetActive(false);
        //stop obstacles
        //stop Road
        movingRoad.x = 0;
        player.projectiles.gameObject.SetActive(false);
    }

    public void MiniGameResume()
    {
        //continue player
        player.speed = 3;
        //continue city
        movingCity.x = 0.2f;
        //continue bonuses
        bonusesObject.gameObject.SetActive(true);
        //continue obstacles
        //continue Road
        movingRoad.x = 0.8f;
        player.projectiles.gameObject.SetActive(true);
    }

    public void DiamondClick()
    {
        gameShop.Coins += 1;
        //oinsText.text = gameShop.Coins.ToString();
        SetCoinText();
        PlayerPrefs.SetInt("coins", gameShop.Coins);
    }

    public void LightningTimer()
    {
        if(timerLightning < maxTimerLightning)
        {
            timerLightning += Time.deltaTime;
        }
        else if(timerLightning >= maxTimerLightning)
        {
            isLightningEffect = false;
            scoreNumSpeed = 1;
            timerLightning = 0;
        }
    }

    public void RemoveCoins()
    {
        gameShop.Coins -= 200;
        //   coinsText.text = gameShop.Coins.ToString();
        SetCoinText();
        PlayerPrefs.SetInt("coins", gameShop.Coins);

     //   startCoins = gameShop.Coins - 200;
    }

    public void TryAgain()
    {
        player.lives = startLives;
        player.transform.position = new Vector3(player.transform.position.x, 0, player.transform.position.z);
        RemoveCoins();
        ResumeGame();
        gameScreen.SetActive(true);
        gameOverScreen.SetActive(false);

    }

    public void DifficultyLevel()
    {
        if(scoreNum < 100)
        {
            currentLevelSpeed = LevelSpeed.zero;

            ChangeDifficultyLevel();
        }
        else if(scoreNum > 100 && scoreNum < 200)
        {
            currentLevelSpeed = LevelSpeed.first;

            ChangeDifficultyLevel();
        }
        else if(scoreNum > 200 && scoreNum < 300)
        {
            currentLevelSpeed = LevelSpeed.second;

            ChangeDifficultyLevel();
        }
        else if (scoreNum > 300 && scoreNum < 400)
        {
            currentLevelSpeed = LevelSpeed.third;

            ChangeDifficultyLevel();
        }
        else if (scoreNum > 400 && scoreNum < 500)
        {
            currentLevelSpeed = LevelSpeed.fourth;

            ChangeDifficultyLevel();
        }
        else if (scoreNum > 500)
        {
            currentLevelSpeed = LevelSpeed.fifth;

            ChangeDifficultyLevel();
        }
    }

    public void ChangeDifficultyLevel()
    {
        if(currentLevelSpeed == LevelSpeed.zero)
        {
            movingCity.x = 0.2f;
            movingRoad.x = 0.8f;
        }
        else if (currentLevelSpeed == LevelSpeed.first)
        {
            movingCity.x = 0.4f;
            movingRoad.x = 1f;
        }
        else if (currentLevelSpeed == LevelSpeed.second)
        {
            movingCity.x = 0.6f;
            movingRoad.x = 1.2f;
        }
        else if (currentLevelSpeed == LevelSpeed.third)
        {
            movingCity.x = 0.8f;
            movingRoad.x = 1.4f;
        }
        else if (currentLevelSpeed == LevelSpeed.fourth)
        {
            movingCity.x = 1f;
            movingRoad.x = 1.6f;
        }
        else if (currentLevelSpeed == LevelSpeed.fifth)
        {
            movingCity.x = 1.2f;
            movingRoad.x = 1.8f;
        }
    }

    public void GameWin()
    {
        gameScreen.SetActive(false);
        gameWinScreen.SetActive(true);
     //   Invoke("LoadMenu", 3);
    }

    public void GameLose()
    {
        if (gameShop.Coins > 200)
        {
            tryAgainBtn.interactable = true;
        }
        else
        {
            tryAgainBtn.interactable = false;
        }
        gameOverScreen.SetActive(true);
        //   gameScreen.SetActive(false);
        PauseGame();
        gameOverScore.text = scoreTxt[0].text;
        coinsEarned += Mathf.Abs(gameShop.Coins - startCoins);
        gameOverCoins.text = coinsEarned.ToString();
     //   Invoke("LoadMenu", 3);
    }

    public void GetReward()
    {

        gameShop.Coins += 500;
        //    coinsText.text = gameShop.Coins.ToString();
        SetCoinText();
        PlayerPrefs.SetInt("coins", gameShop.Coins);
    }

    public void LoadMenu()
    {
        ResumeGame();
        SceneManager.LoadScene("main");
    }

    public enum LevelSpeed
    {
        zero,
        first,
        second,
        third,
        fourth,
        fifth
    }
}
