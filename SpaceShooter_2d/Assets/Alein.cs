using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Alein : MonoBehaviour
{
    public float speed = 5f;
    public AlienType alienType;
    [SerializeField] float hitPoints, maxHitPoints;
    [SerializeField] SpriteRenderer alienSprite;
    [SerializeField] Image healthBar;

    private void OnEnable()
    {
        StartCoroutine(IMovementStart());
        AlienHealth();
    }

    IEnumerator IMovementStart()
    {
        while (!GameManager.instance.isGameOver)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Missile"))
        {
            hitPoints--;
            if (this.gameObject.activeSelf)
                AnimateHitTaken();
            healthBar.color = Color.Lerp(Color.red, Color.green,0.25f);
            healthBar.fillAmount = hitPoints / maxHitPoints;
            //  GameManager.instance.Score += 10;
            if (hitPoints <= 0)
            {

                this.gameObject.SetActive(false);
                GameManager.instance.Score++;
            }


        }
        if (collision.CompareTag("Player"))
        {
            this.gameObject.SetActive(false);
            GameManager.instance.PlayerLife--;
            //  collision.gameObject.SetActive(false);
        }
    }
    void AlienHealth()
    {
        switch (alienType)
        {
            case AlienType.beginner:
                maxHitPoints = 1;

                break;
            case AlienType.intermediate:
                maxHitPoints = 3;
                break;
            case AlienType.hard:
                maxHitPoints = 5;
                break;
            case AlienType.nighmare:
                maxHitPoints = 10;
                break;
            default:
                break;
        }
        hitPoints = maxHitPoints;

       
        healthBar.color = Color.green;
        healthBar.fillAmount = 1;


    }
    #region Animation when Hit was Taken
    void AnimateHitTaken()
    {
        StartCoroutine(IAnimateHitTaken());
    }
    
    IEnumerator IAnimateHitTaken()
    {
        Color color = alienSprite.color;
        float timer = 0.5f;

        while (timer > 0f)
        {
            timer -= 0.15f;
            yield return new WaitForSeconds(Time.deltaTime);
            color.a = timer;
            alienSprite.color = color;

        }
        color.a = 1f;
        alienSprite.color = color;
    }
    #endregion
}
public enum AlienType
{
    beginner,
    intermediate,
    hard,
    nighmare
}
