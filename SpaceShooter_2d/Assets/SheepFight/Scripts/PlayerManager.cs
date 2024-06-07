using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SheepFight
{

    public class PlayerManager : MonoBehaviour
    {
        public RaycastHit2D rayHit;

        public GameObject playerObj;
        public GameObject enemyObj;

        public List<GameObject> playerObjsList;
        public List<GameObject> enemyObjsList;
        public GameObject activePlayerObj;

        public GameObject players, enemies;

        public GameObject pathObj, startPanel;
        public GameObject winPanel, losePanel;

        public Image enemyFillImage, playerFillImage;
        public Text enemyPoints, playerPoints;

        public static PlayerManager instance;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }
        private void Start()
        {
            for (int i = 0; i < 15; i++)
            {
                GameObject go = Instantiate(playerObjsList[i], players.transform);

                go.SetActive(false);
                playerObjsList.Add(go);

                GameObject eo = Instantiate(enemyObjsList[i], enemies.transform);
                eo.SetActive(false);
                enemyObjsList.Add(eo);
            }
        }
        public GameObject ActiveEnemy()
        {
            return enemyObjsList.Find(x => !x.activeSelf);
        }
        public GameObject ActivePlayer()
        {
            return playerObjsList.Find(x => !x.activeSelf);
        }


        public GameObject UpNextPlayer()
        {
            int index = Random.Range(0, 4);
            playerObj = playerObjsList[index];
            return playerObj;
        }
        public GameObject UpNextEnemy()
        {
            int index = Random.Range(0, 4);
            enemyObj = enemyObjsList[index];
            return enemyObj;
        }

        float waitTime;
        float _waitTime;
        float maxWaitTime = 2f;
        bool cantSpawnPlayer = false;
        bool cantSpawnEnemy = false;
        IEnumerator IWaitForCoolDown()
        {
            float fill;

            while (waitTime < maxWaitTime)
            {
                waitTime += Time.deltaTime;
                fill = waitTime / maxWaitTime;
                playerFillImage.fillAmount = fill;
                yield return new WaitForSeconds(Time.deltaTime);
            }
            cantSpawnPlayer = false;
            waitTime = 0f;
            playerFillImage.fillAmount = 0f;
        }
        IEnumerator IWaitForCoolDownEnemy()
        {

            float fill;
            while (_waitTime < maxWaitTime)
            {
                _waitTime += Time.deltaTime;
                fill = _waitTime / maxWaitTime;
                enemyFillImage.fillAmount = fill;
                yield return new WaitForSeconds(Time.deltaTime);
            }
            cantSpawnEnemy = false;
            _waitTime = 0f;
            enemyFillImage.fillAmount = 0f;
        }

        public bool isPlayer;
        public bool isEnemy;
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                //isPaused = true;
                Resume();
            }




            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit raycastHit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out raycastHit, 100f))
                {
                    if (raycastHit.transform.gameObject.CompareTag("Path") && IsFromPlayer() && !cantSpawnPlayer)
                    {
                        UpNextPlayer();
                        isPlayer = true;
                        isStartedFromPlayer = true;
                        cantSpawnPlayer = true;
                        GameObject activePlayerObj = ActivePlayer();
                        activePlayerObj.SetActive(true);
                        activePlayerObj.transform.position = raycastHit.transform.gameObject.transform.GetChild(0).transform.position;
                        StartCoroutine(IWaitForCoolDown());
                    }
                    else if (raycastHit.transform.gameObject.CompareTag("Path") && !IsFromPlayer() && !cantSpawnEnemy)
                    {
                        UpNextEnemy();
                        isStartedFromPlayer = false;
                        cantSpawnEnemy = true;
                        GameObject activeEnemy = ActiveEnemy();
                        activeEnemy.SetActive(true);
                        isEnemy = false;
                        activeEnemy.transform.position = raycastHit.transform.gameObject.transform.GetChild(1).transform.position;
                        StartCoroutine(IWaitForCoolDownEnemy());
                    }
                    CheckPath(raycastHit.transform.gameObject);
                }


            }
        }
        public int pathNo;

        public int CheckPath(GameObject path)
        {
            pathNo = path.GetComponent<Path>().index;
            return pathNo;
        }


        public bool isStartedFromPlayer = false;
        public bool IsFromPlayer()
        {
            return Input.mousePosition.y < Screen.height / 2f;
        }

        public bool isWin;
        public void GameOver()
        {

            if (isWin)
                winPanel.SetActive(true);
            else
                losePanel.SetActive(true);
            pathObj.SetActive(false);
            StopAllCoroutines();

        }
        public void Home()
        {
            isWin = false;
            startPanel.SetActive(true);
            pathObj.SetActive(false);
            ResetValues();
            StopAllCoroutines();
        }
        private void ResetValues()
        {
            for (int i = 0; i < GameManager.instance.pWeights.Length; i++)
            {
                GameManager.instance.pWeights[i] = 0;
                GameManager.instance.eWeights[i] = 0;
            }
            GameManager.instance.maxEnemyBarnHealth = 30;
            GameManager.instance.maxPlayerBarnHealth = 30;
            if (Time.timeScale != 1)
                Time.timeScale = 1f;

        }
        [SerializeField] bool isPaused;
        public GameObject pausePanel;
        public void Resume()
        {
            print(" here");
            if (isPaused)
            {
                isPaused = false;
                pausePanel.SetActive(false);
                Time.timeScale = 1f;
            }
            else
            {
                isPaused = true;
                pausePanel.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }
}
