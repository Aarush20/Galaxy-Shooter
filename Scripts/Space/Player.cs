using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;
    private float _speedMultiplier = 2;
    [SerializeField] private GameObject _laserPrefabs;
    [SerializeField] private float _fireRate = 0.5f;
     private float _canFire = -1f;
    [SerializeField] private int _lives = 3;
    private SpawnManager _spawnManager;
    [SerializeField] private GameObject _tripleShot;
    [SerializeField] private GameObject _shieldVisualiser;
    [SerializeField] private GameObject _leftEngine, _rightEngine;
    private int _score, _bestScore = 0;
    private UIManager _uiManager;
    [SerializeField] private AudioClip _laserClip;
    private AudioSource _audioSource;
    private GameManager _gameManager;
    
    
    private bool isTripleShotActive = false;
    private bool isSpeedBoostActive = false;
    private bool isSheildActive = false;

    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioSource = GetComponent<AudioSource>();
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        

        if (_spawnManager == null)
        {
            Debug.Log("The SpawnManger is NULL");
        }
        if (_uiManager == null)
        {
            Debug.LogError("The Ui manager is null");
        }
        if(_audioSource == null)
        {
            Debug.LogError("Audio Source is missing on player!");
        }
        else
        {
            _audioSource.clip = _laserClip;
        }
        if (_gameManager.isCoOpMode == false)
        {
            transform.position = new Vector3(0, 0, 0);// taking current position and assining to a new position
        }

    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire) // time.time is how much time has passed since the start os the game
        {
            FireLaser();
        }


    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");// take a variable and giving it an axis
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        //transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);// change the position == vector(1,0,0)* horizontalInput* speed * real time
        //transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);
            
        transform.Translate(direction * _speed * Time.deltaTime);//optimal option for the above 
       
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);// this code clamp to min and max

        //if (transform.position.y >= 0)
        // {
        //     transform.position = new Vector3(transform.position.x, 0, 0);
        // }
        // else if (transform.position.y <= -3.8f)
        // {
        //     transform.position = new Vector3(transform.position.x, -3.8f, 0);
        // }
        if (transform.position.x <= -11)
        {
            transform.position = new Vector3(11, transform.position.y, 0);
        }
        else if (transform.position.x >= 11)
        {
            transform.position = new Vector3(-11, transform.position.y, 0);
        }
    }

     void FireLaser()
     {
        
            _canFire = Time.time + _fireRate;
        if(isTripleShotActive == false)
        {
            Instantiate(_laserPrefabs, transform.position + new Vector3(0, 1.1f, 0), Quaternion.identity);
        }
        else
        {
            Instantiate(_tripleShot, transform.position + new Vector3(1.601f, -0.406f , 0), Quaternion.identity);
        }
        _audioSource.Play();
            

     }

    public void Damage()
    {
       
        if (isSheildActive == true)
        {
            isSheildActive = false;
            _shieldVisualiser.SetActive(false);
            return;// stops the program here

        }
        _lives--;
        if(_lives == 2)
        {
            _leftEngine.SetActive(true);
        }
        else if (_lives == 1)
        {
            _rightEngine.SetActive(true);
        }

        _uiManager.UpdateImage(_lives);
        if (_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
            /*if(_score >= _bestScore)
            {
                _bestScore = _score
                _uiManager.BestScore(_score);
            }*/
            
        }
    }

    public void TripleShotActive()
    {
        isTripleShotActive = true;
        StartCoroutine(TripleShotPowerupCooldown());
    }
    public void SpeedBoostActive()
    {
        isSpeedBoostActive = true;
        _speed *= _speedMultiplier;
        StartCoroutine(SpeedBoostPowerupCooldown());
    }
    public void SheildActive()
    {
        isSheildActive = true;
        _shieldVisualiser.SetActive(true);
    }


    IEnumerator TripleShotPowerupCooldown()
    {
        yield return new WaitForSeconds(5f);
        isTripleShotActive = false;
        
    }
    IEnumerator SpeedBoostPowerupCooldown()
    {
        yield return new WaitForSeconds(5f);
        isSpeedBoostActive = false;
        _speed /= _speedMultiplier;
    }
    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }
    
}
