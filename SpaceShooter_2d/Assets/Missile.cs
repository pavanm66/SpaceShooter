using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    Vector3 direction;
    float speed;
    public void Initialize(Vector2 dir, float _speed,Vector2 initPos)
    {
        transform.position = initPos;
        this.gameObject.SetActive(true);
        
        direction = dir;
        speed = _speed;
    }
    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
        if (transform.position.x > 7.83f)
            this.gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Alien"))
        {
           
            this.gameObject.SetActive(false);
        }
    }
}
