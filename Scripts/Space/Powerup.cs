using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private int _powerupId;
    [SerializeField] private AudioClip _clip;
    // Start is called before the first frame update
    void Start()
    {
        float _randomX = Random.Range(-10f, 10f);

        transform.position = new Vector3(_randomX, 8f, 0);
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if(transform.position.y <= -5f)
        {
            Destroy(this.gameObject);
        }



    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag =="Player")
        {
            Player player = other.gameObject.GetComponent<Player>();
            AudioSource.PlayClipAtPoint(_clip, transform.position);
            if (player != null)
            {
                
                switch(_powerupId)
                {
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.SpeedBoostActive();
                        break;
                    case 2:
                        player.SheildActive();
                        break;

                }
            }
            Destroy(this.gameObject);
        }
    }

}
