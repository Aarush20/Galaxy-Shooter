using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Player _player;
    private Animator _animator;
    private AudioSource _audioSource;
    [SerializeField] private GameObject _enemyLaserPrefab;
    private float _fireRate = 3f;
    private float _canFire= -1;


    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _audioSource = GetComponent<AudioSource>();
        if(_player == null)
        {
            Debug.LogError("No Player Commponent");
        }
        _animator = GetComponent<Animator>();
        transform.position = new Vector3(Random.Range(-10f, 10f), 8f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMovement();
        
        if(Time.time > _canFire)
        {
            _fireRate = Random.Range(4f, 7f);
            _canFire = Time.time + _fireRate;
            GameObject enemyLaser =   Instantiate(_enemyLaserPrefab, transform.position, Quaternion.identity);
            Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();

            for(int i = 0; i<lasers.Length; i++)
            {
                lasers[i].AssignEnemyLaser();
            }
            if(this.gameObject == null)
            {
                Destroy(enemyLaser);
            }
           
        }
       
    }
    void EnemyMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y <= -5f)
        {
            float randomX = Random.Range(-10f, 10f);
            transform.position = new Vector3(randomX, 8f, 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();

            }
            _speed = 0;
            _animator.SetTrigger("OnEnemyDeath");
            _audioSource.Play();
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 2.8f);
        }
        else if (other.tag =="Laser" )
        {
            Destroy(other.gameObject);
            if (_player != null)
            {
                _player.AddScore(Random.Range(5,11));
            }
            _animator.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _audioSource.Play();
            
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 2.8f);
               
        }
    }
    
}
