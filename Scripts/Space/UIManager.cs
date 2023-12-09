using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _score ,_bestScore ;
    private int _scoreUpdate = 0;
    [SerializeField] private Image _livesImg;
    [SerializeField] private Sprite[] _livesSprite;
    [SerializeField] private TMP_Text _gameOverText;
    [SerializeField] private TMP_Text _restartText;
    private GameManager _gameManager;
    

    // Start is called before the first frame update
    void Start()
    {
        _score.text = "Score :" + _scoreUpdate;
        _gameOverText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        if (_gameManager == null)
        {
            Debug.LogError("Not found Game Manager");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateScore(int playerScore)
    {
        _score.text = "Score :" + playerScore.ToString();
    }
    /* public void BestScore(int best)
    {
        _bestScore.text = "Best Score :" + best.ToString();
    }*/
   
    public void UpdateImage(int currentLives)
    {
        _livesImg.sprite = _livesSprite[currentLives];
        if (currentLives <= 0 )
        {
            GameOverText();
        }
    }

     void GameOverText()
     {
        _gameManager.GameOver();
        _gameOverText.gameObject.SetActive(true);
        _restartText.gameObject.SetActive(true);
        StartCoroutine(GameOverTextFlicker());
        StartCoroutine(RestarTextFlicker());

     }
    IEnumerator GameOverTextFlicker()
    {
        while(true)
        {
            _gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
        
    }
    IEnumerator RestarTextFlicker()
    {
        while(true)
        {
            _restartText.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _restartText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }


    public void ResumePlay()
    {
        _gameManager.PauseMenuPanel();
        Time.timeScale = 1f;
    }   
    public void MainMenuButton()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }
}
