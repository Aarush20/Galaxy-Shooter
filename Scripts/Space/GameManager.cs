using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool _isGameOver;
    public bool isCoOpMode = false;
    [SerializeField] private GameObject _pausePanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(1);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _pausePanel.SetActive(true);
            Time.timeScale = 0f;

        }
    }
    public void PauseMenuPanel()
    {
        _pausePanel.SetActive(false);
    }
    public void GameOver()
    {
        _isGameOver = true;
    }
}
