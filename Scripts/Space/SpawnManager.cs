using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject[] _powerups;
    [SerializeField] private GameObject _enemyContainer;
    
    private bool _stopSpawning = false;
    // Start is called before the first frame update
   
    public void StartSpawning()
    {
        StartCoroutine(TimeToSpawn());
        StartCoroutine(PowerupSpawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator TimeToSpawn()
    {
        yield return new WaitForSeconds(3f);
        while(_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-10f, 10f), 8, 0);
            GameObject newEnemy =  Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(4f);
        }    

    }

    IEnumerator PowerupSpawn()
    {
        yield return new WaitForSeconds(3f);
        while (_stopSpawning == false)
        {
            Vector3 powerupSpawn = new Vector3(Random.Range(-10f, 10f), 8f, 0);
            int randomPowerup = Random.Range(0, 3);
            GameObject newPowerup = Instantiate(_powerups[randomPowerup], powerupSpawn, Quaternion.identity);
            
            yield return new WaitForSeconds(Random.Range(5f, 8f));
        }    

    }
    public void OnPlayerDeath()
    {
        _stopSpawning = true;

        //Destroy(this.gameObject);
    }
}
