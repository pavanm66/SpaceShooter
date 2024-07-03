using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AleinManager : MonoBehaviour
{
    public GameObject alienPrefab, alien_intermediatePrefab;
    public List<GameObject> alienList;
    public Transform spawnPoint;
    [SerializeField] private float minHeight;
    [SerializeField] private float maxHeight;

    // Start is called before the first frame update
    public void StartGame(List<GameObject> alienList, AlienType alienType)
    {
        // alienList = new List<GameObject>();
        for (int i = 0; i < alienList.Count; i++)
        {
            GameObject alien = Instantiate(alienPrefab, transform);
            GameObject alien_intermediate = Instantiate(alien_intermediatePrefab, transform);
            alien.SetActive(false);
            alien_intermediate.SetActive(false);
            alienList.Add(alien);
            alienList.Add(alien_intermediate);
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
            GameObject alien = GetAliensFromPool();
            alien.SetActive(true);
            alien.transform.position = new Vector2(spawnPoint.position.x, Random.Range(minHeight, maxHeight));
            yield return new WaitForSeconds(Random.Range(1f, 3.5f));
        }
        StopCoroutine(ISpawnAliens());
    }
}
