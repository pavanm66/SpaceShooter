using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform canVasObj;
    public Transform shootPoint;
    public GameObject missile;
    public float fireTimer;
    public float fireRate;

    public List<GameObject> missileList;

    private void Start()
    {
        missileList = new List<GameObject>();
        for (int i = 0; i < 30; i++)
        {
            GameObject bullet = (GameObject)Instantiate(missile, shootPoint.position, transform.rotation);
            bullet.SetActive(false);
            missileList.Add(bullet);
        }

    }
    void Update()
    {

        PlayerMovement();
      
        if (Input.GetKey(KeyCode.Space))
        {
            fireTimer += Time.deltaTime;
            if (fireTimer > fireRate)
            {
                FireMissile();
                fireTimer = 0;
            }
        }
    }
    public float verticalSpeed = 3f;
    public float horSpeed = 3f;
    public void PlayerMovement()
    {
        transform.position += new Vector3(0f, Input.GetAxis("Vertical"), 0f) * Time.deltaTime * verticalSpeed;
        if (transform.position.y > 4f)
        {
            transform.position = new Vector3(transform.position.x, 4f, transform.position.z);
        }
        else if (transform.position.y < -4f)
            transform.position = new Vector3(transform.position.x, -4f, transform.position.z);

        transform.position += new Vector3(Input.GetAxis("Horizontal"), 0f, 0f) * Time.deltaTime * horSpeed;
        if (transform.position.x < -7.83f)
        {
            transform.position = new Vector3(-7.83f, transform.position.y, transform.position.z);

        }
        else if (transform.position.x > 7.83f)
        {
            transform.position = new Vector3(7.83f, transform.position.y, transform.position.z);

        }

    }
   

    public float missileSpeed;
    public void FireMissile()
    {
        Vector2 direction = Vector2.right;
        GameObject bullet = GetMissileFromPool();

        bullet.GetComponent<Missile>().Initialize(direction, missileSpeed, shootPoint.position);

    }
    GameObject GetMissileFromPool()
    {
        
        return missileList.Find(x => !x.activeSelf);
    }

}
