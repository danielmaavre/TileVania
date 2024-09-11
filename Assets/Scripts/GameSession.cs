using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] int score = 0;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;

    int scoreCount = 0;
    CoinPickup coinPickup;
    void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if(numGameSessions > 1){
            Destroy(gameObject);
        }else{
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start() {
        livesText.text = "Lives: "+playerLives.ToString();
        scoreText.text = "Score: "+score;
    }


    public void ProcessPlayerDeath(){
        if(playerLives > 1){
            TakeLife();
        }else{
            ResetGameSession();            
            score = 0;
        }
    }

    private void TakeLife()
    {
        playerLives --;
        int currentSceneIdx = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIdx);
        livesText.text = "Lives: "+playerLives.ToString();
    }

    private void ResetGameSession()
    {
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    public void AddToScore(int pointsToAdd){
        score += pointsToAdd;
        scoreText.text = "Score: "+score;
    }
}
