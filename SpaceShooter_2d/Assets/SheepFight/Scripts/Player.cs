using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SheepFight
{

    public class Player : MonoBehaviour
    {

        public PlayerCharacteristics pc;
        public SpriteRenderer icon;
        int damage;
        int weight;

        private void OnEnable()
        {
            isPushing = false;
            damage = pc.damage;
            weight = pc.weight;
            icon.sprite = pc.icon;
            direction = pc.direction;
        }


        private void Update()
        {



            if (isPushing)
            {
                AdjustVelocity();
            }
            transform.Translate(Vector3.up * vel * direction * Time.deltaTime);

        }

        Ray ray;
        private RaycastHit HitInfo;
        [SerializeField] private int direction;
        public float rayLength;
        public bool isPushing;
        public bool isPlayer;


        //public int pathIndex;

        void AdjustVelocity()
        {
            direction = GameManager.instance.CheckPushingDirection(PlayerManager.instance.pathNo);
            vel = 0.3f;
        }

        float vel = 0.5f;

        void CheckCollision()
        {
            Vector3 origin = transform.position;
            ray = new Ray(origin, Vector3.up * direction * rayLength);
            if (Physics.Raycast(ray, out HitInfo, rayLength))
            {
                GameObject other = HitInfo.collider.gameObject;
                if (other.CompareTag("Sheep") && !isPushing)
                {

                    Player otherPlayer = other.GetComponent<Player>();
                    if (isPlayer && !otherPlayer.isPlayer)
                    {
                        GameManager.instance.pWeights[PlayerManager.instance.pathNo - 1] += weight;
                        GameManager.instance.eWeights[PlayerManager.instance.pathNo - 1] += otherPlayer.weight;

                    }
                    if (isPlayer && otherPlayer.isPlayer)
                    {
                        GameManager.instance.pWeights[PlayerManager.instance.pathNo - 1] += weight;
                    }
                    if (!isPlayer && !otherPlayer.isPlayer) GameManager.instance.eWeights[PlayerManager.instance.pathNo - 1] += weight;

                    isPushing = true;

                }
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Complete"))
            {
                if (isPlayer)
                {
                    if (isPushing)
                    {
                        GameManager.instance.pWeights[PlayerManager.instance.pathNo - 1] -= weight;
                        if (GameManager.instance.pWeights[PlayerManager.instance.pathNo - 1] <= 0)
                        {
                            GameManager.instance.pWeights[PlayerManager.instance.pathNo - 1] = 0;
                        }

                    }
                    if (other.transform.position.y >= 3.8f)
                    {
                        GameManager.instance.maxEnemyBarnHealth -= damage;
                        if (GameManager.instance.maxEnemyBarnHealth < 0)
                        {
                            GameManager.instance.maxEnemyBarnHealth = 0;
                            PlayerManager.instance.isWin = true;
                            PlayerManager.instance.GameOver();
                        }
                        PlayerManager.instance.enemyPoints.text = "Points : " + GameManager.instance.maxEnemyBarnHealth;
                    }
                }
                else
                {
                    if (isPushing)
                    {
                        GameManager.instance.eWeights[PlayerManager.instance.pathNo - 1] -= weight;
                        if (GameManager.instance.eWeights[PlayerManager.instance.pathNo - 1] <= 0)
                        {
                            GameManager.instance.eWeights[PlayerManager.instance.pathNo - 1] = 0;


                        }
                    }
                    if (other.transform.position.y <= -3.82f)
                    {
                        GameManager.instance.maxPlayerBarnHealth -= damage;
                        if (GameManager.instance.maxPlayerBarnHealth < 0)
                        {
                            GameManager.instance.maxPlayerBarnHealth = 0;
                            PlayerManager.instance.isWin = false;
                            PlayerManager.instance.GameOver();
                        }
                    }
                    PlayerManager.instance.playerPoints.text = "Points : " + GameManager.instance.maxPlayerBarnHealth;
                }
                gameObject.SetActive(false);
            }
        }
        private void OnCollisionEnter(Collision collision)
        {

            if (collision.gameObject.CompareTag("Sheep"))
                CheckCollision();


        }







    }
}
