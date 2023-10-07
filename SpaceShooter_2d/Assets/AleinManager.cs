using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AleinManager : MonoBehaviour
{
    public GameObject alienPrefab;
    public List<GameObject> alienList;
    public Transform spawnPoint;
    [SerializeField] private float minHeight;
    [SerializeField] private float maxHeight;

    // Start is called before the first frame update
    void Start()
    {
        alienList = new List<GameObject>();
        for (int i = 0; i < 20; i++)
        {
            GameObject alien = Instantiate(alienPrefab, transform);
            alien.SetActive(false);
            alienList.Add(alien);
        }
       // StartCoroutine(ISpawnAliens());
    }
    GameObject GetAliensFromPool()
    {
        return alienList.Find(x => !x.activeSelf);
    }

  /*  IEnumerator ISpawnAliens()
    {
        while (!GameManager.instance.isGameOver)
        {
            GameObject alien = GetAliensFromPool();
            alien.SetActive(true);
            alien.transform.position = new Vector2(spawnPoint.position.x, Random.Range(minHeight, maxHeight));
            yield return new WaitForSeconds(Random.Range(1f,3.5f));
        }
        StopCoroutine(ISpawnAliens());
    }*/
}
