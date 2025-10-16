using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Transform canVasObj;
    public Transform shootPoint;
    public GameObject missile;
    public float fireTimer;
    public float fireRate = 0.5f;
    public Vector2 spawnPos;

    public List<GameObject> missileList;
 
    public float missileSpeed;

   
   
    private bool isFiring;
    [SerializeField] JoystickManager joystickManager;

    private void Start()
    {
        spawnPos = transform.position;
        missileList = new List<GameObject>();

        // Initialize the missile pool
        for (int i = 0; i < 30; i++)
        {
            GameObject bullet = Instantiate(missile, shootPoint.position, transform.rotation);
            bullet.SetActive(false);
            missileList.Add(bullet);
        }
    }

    private void Update()
    {

        // Fire missile when space is held
        fireTimer += Time.deltaTime;
        if (isFiring && fireTimer > fireRate)
        {
            FireMissile();
            fireTimer = 0;
        }
    }

    public void FireMissile()
    {
        Vector2 direction = Vector2.right; // You may adjust this to match your game's logic
        GameObject bullet = GetMissileFromPool();

        bullet.GetComponent<Missile>().Initialize(direction, missileSpeed, shootPoint.position);
    }

    GameObject GetMissileFromPool()
    {
        return missileList.Find(x => !x.activeSelf);
    }

   

    public void StartFiring()
    {
        isFiring = true;
    }

    public void StopFiring()
    {
        isFiring = false;
    }
}
