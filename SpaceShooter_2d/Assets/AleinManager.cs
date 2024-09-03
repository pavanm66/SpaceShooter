using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AleinManager : MonoBehaviour
{
    public GameObject alienPrefab, alien_intermediatePrefab;
    public List<GameObject> alienList = new List<GameObject>();
    public WaveManager waveManager;
    
    public Transform spawnPoint;
    [SerializeField] private float minHeight;
    [SerializeField] private float maxHeight;

    // Start is called before the first frame update
    public void StartGame( int enemyCount, AlienType alienType)
    {
        for (int i = 0; i < enemyCount; i++)
        {
            GameObject alien = Instantiate(alienPrefab, transform);
            alien.SetActive(false);
            alienList.Add(alien);
        }
        StartCoroutine(ISpawnAliens());
    }

    //this is to get aliens from the pool of aliens which were instantiated at the start 
    GameObject GetAliensFromPool()
    {
        return alienList.Find(x => !x.activeSelf);
    }
    //This is for spawning aliens
    IEnumerator ISpawnAliens()
    {
        while (!GameManager.instance.isGameOver)
        {
            // Check if the number of active aliens is less than 3
            int activeAliens = alienList.Count(x => x.activeSelf);
            if (activeAliens < 3)
            {
                GameObject alien = GetAliensFromPool();
                if (alien != null)
                {
                    alien.SetActive(true);
                    alien.transform.position = new Vector2(spawnPoint.position.x, Random.Range(minHeight, maxHeight));
                }
            }
            yield return new WaitForSeconds(Random.Range(1f, 3.5f));
        }
        StopCoroutine(ISpawnAliens());
    }
}
