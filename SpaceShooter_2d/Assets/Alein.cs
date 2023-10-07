using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alein : MonoBehaviour
{
    public float speed = 5f;
    private void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Missile"))
        {
          //  GameManager.instance.Score += 10;
            this.gameObject.SetActive(false);
          
           
        }
        if (collision.CompareTag("Player"))
        {
            this.gameObject.SetActive(false);
           /* GameManager.instance.PlayerLife--;
            if (GameManager.instance.PlayerLife == 0)
            {
                collision.gameObject.SetActive(false);
                GameManager.instance.isGameOver = true;
                GameManager.instance.uiManager.gameOverPanel.SetActive(true);
            }*/
        }
    }
}
