using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This is basically an enemy with 5 lasers.
public class Boss : MonoBehaviour
{
    //////////////////////////////////
    ///////////// FIELDS /////////////
    //////////////////////////////////

    [SerializeField] float health = 100;          
    // Time between the shots. It's going to be "kinda" random. So, I'll use the next 2 var. with it.      
    [SerializeField] float shotCounter;                   
    [SerializeField] float minTimeBetweenShots = 0.2f;    
    [SerializeField] float maxTimeBetweenShots = 3f;    
    [SerializeField] List<GameObject> laserPrefabs;
    [SerializeField] float laserSpeed;
    [SerializeField] GameObject explosionVFX;
    [SerializeField] float durationOfExplosion = 1f;
    [SerializeField] AudioClip explosionSound;
    [SerializeField] AudioClip laserSound;
    [SerializeField] GameObject healthPowerUpPrefab;
    [SerializeField] GameObject laserPowerUpPrefab;


    //////////////////////////////////
    ///////// START & UPDATE /////////
    //////////////////////////////////
    void Start()
    {
        shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    void Update()
    {
        CountDownAndShot();
    }


    //////////////////////////////////
    ////////// COLLUSISONS ///////////
    //////////////////////////////////
    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if(!damageDealer){
            return;
        }
        ProcessHit(damageDealer);
    }


    //////////////////////////////////
    //////////// METHODS /////////////
    //////////////////////////////////

    // This method is to process hits and destroy the ships if their hp becomes 0.
    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }

    // This method is to destroy the ships.
    // It'll play "explosionSound" on where the enemy is;
    // destroy the enemy object;
    // play the explosionVFX on where the enemy is;
    // and destroy the variable "explosion" which stands for the explosionVFX's clone;
    private void Die()
    {
        AudioSource.PlayClipAtPoint(explosionSound, transform.position);
        Destroy(gameObject);
        GameObject explosion = Instantiate(explosionVFX, transform.position, transform.rotation);
        Destroy(explosion, durationOfExplosion);
        GameSession gameSession = FindObjectOfType<GameSession>();
        gameSession.AddToScore(100);
        Vector2 laserBoxPos = new Vector2(transform.position.x - 1, transform.position.y + 0.75f);
        GameObject laserBox = Instantiate(laserPowerUpPrefab, laserBoxPos, transform.rotation);
        Vector2 healthBoxPos = new Vector2(transform.position.x, transform.position.y);
        GameObject healthBox = Instantiate(healthPowerUpPrefab, healthBoxPos, transform.rotation);
     }


    // This method is to make count and shot using the "Fire()" method.
    private void CountDownAndShot()
    {
        shotCounter -= Time.deltaTime;  // shotCounter will be decreased by 1 at every single frame.
        if (shotCounter <= 0f)
        {
            Fire();
            shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    // This method is to make enemies fire.
    // When it's called, it'll play the laserSound at where enemy is;
    // then create a clon of the laserPrefab named "laser";
    // and give the laser a velocity to move backwards.
    private void Fire()
    {
        AudioSource.PlayClipAtPoint(laserSound, transform.position, 0.5f);   // play the laserSound, on where the enemy is, at 0,5f volume.
        GameObject laserPrefab = laserPrefabs[UnityEngine.Random.Range(0, laserPrefabs.Count)];
        GameObject laser1 = Instantiate(laserPrefab, transform.position, Quaternion.identity);
        GameObject laser2 = Instantiate(laserPrefab, transform.position, Quaternion.identity);
        GameObject laser3 = Instantiate(laserPrefab, transform.position, Quaternion.identity);
        GameObject laser4 = Instantiate(laserPrefab, transform.position, Quaternion.identity);
        GameObject laser5 = Instantiate(laserPrefab, transform.position, Quaternion.identity);
        laser1.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -laserSpeed);
        laser2.GetComponent<Rigidbody2D>().velocity = new Vector2(-4, -laserSpeed);
        laser3.GetComponent<Rigidbody2D>().velocity = new Vector2(-2, -laserSpeed);
        laser4.GetComponent<Rigidbody2D>().velocity = new Vector2(2, -laserSpeed);
        laser5.GetComponent<Rigidbody2D>().velocity = new Vector2(4, -laserSpeed);
    }
}
