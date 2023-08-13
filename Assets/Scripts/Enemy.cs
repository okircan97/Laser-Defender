using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //////////////////////////////////
    ///////////// FIELDS /////////////
    //////////////////////////////////

    [SerializeField] float health = 100;
    // Time between the shots.                
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] GameObject laserPrefab;
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
        // Get a random value for the shot counter.
        shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }

    void Update()
    {
        CountDownAndShot();
    }


    //////////////////////////////////
    /////////// COLLUSIONS ///////////
    //////////////////////////////////   

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer)
        {
            return;
        }
        ProcessHit(damageDealer);
    }


    //////////////////////////////////
    ///////////// METHODS ////////////
    //////////////////////////////////   

    // This method is to process the hits on the enemy objects.
    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Die();
        }
    }

    // This method is to destroy the enemies while playing the relevant
    // audios and SFXs. It'll also make them drop power ups.
    private void Die()
    {
        AudioSource.PlayClipAtPoint(explosionSound, transform.position);
        Destroy(gameObject);
        GameObject explosion = Instantiate(explosionVFX, transform.position, transform.rotation);
        Destroy(explosion, durationOfExplosion);
        GameSession gameSession = FindObjectOfType<GameSession>();
        gameSession.AddToScore(1);
        float randFactor = UnityEngine.Random.Range(-0.75f, 0.75f);
        Vector2 laserBoxPos = new Vector2(transform.position.x + randFactor, transform.position.y + randFactor);
        GameObject laserBox = Instantiate(laserPowerUpPrefab, laserBoxPos, transform.rotation);
        GameObject helpBox = Instantiate(healthPowerUpPrefab, transform.position, transform.rotation);
    }

    // This method is to make the enemies count down and fire. 
    private void CountDownAndShot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            Fire();
            shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    // This method is to make the enemies fire.
    private void Fire()
    {
        AudioSource.PlayClipAtPoint(laserSound, transform.position, 0.5f);
        GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -laserSpeed);
    }

    // This method is to increase the enemy hp.
    public void UpgradeEnemy()
    {
        health = health + health / 10;
    }
}
