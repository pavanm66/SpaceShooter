using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alein : MonoBehaviour
{
    public float speed = 5f;
    public AlienType alienType;

    private void OnEnable()
    {
        StartCoroutine(IMovementStart());
        
    }
   
    IEnumerator IMovementStart()
    {
        while (!GameManager.instance.isGameOver)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
         /*   if (transform.position.x < -7.8f)
            {
                this.gameObject.SetActive(false);
            }*/

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Missile"))
        {
            //  GameManager.instance.Score += 10;
            this.gameObject.SetActive(false);
            GameManager.instance.Score++;


        }
        if (collision.CompareTag("Player"))
        {
            this.gameObject.SetActive(false);
            GameManager.instance.PlayerLife--;
            if (GameManager.instance.PlayerLife == 0)
            {
                collision.gameObject.SetActive(false);
                GameManager.instance.isGameOver = true;
                GameManager.instance.uiManager.gameOverPanel.SetActive(true);
            }

        }
    }
}
public enum AlienType
{
    beginner,
    intermediate,
    hard,
    nighmare
}
