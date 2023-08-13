using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //////////////////////////////////
    ///////////// FIELDS /////////////
    //////////////////////////////////    

    [Header("Player")]
    float slowMoveSpeed = 3f;
    float constMoveSpeed = 10f;
    [SerializeField] float moveSpeed = 10f;
    // We'll use this value to play over the borders in the method SetUpMoveBoundaries(). 
    [SerializeField] float padding = 0.5f;       
    [SerializeField] float health = 200;
    [SerializeField] GameObject explosionVFX;
    [SerializeField] float durationOfExplosion = 1f;
    [SerializeField] AudioClip explosionSound;
    [SerializeField] GameObject engineFirePrefab;

    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float laserSpeed;            
    [SerializeField] float laserFiringPeriod = 0.05f;
    [SerializeField] AudioClip laserSound;
    Coroutine FiringCoroutine;
    [SerializeField] GameObject backFirePrefab;   

    // The min and max boundiries of the scene.
    float xMin;  
    float xMax; 
    float yMin; 
    float yMax;  


    //////////////////////////////////
    ///////// START & UPDATE /////////
    //////////////////////////////////
    
    void Start()
    {
        SetUpMoveBoundaries();
        DamageDealer damageDealer = laserPrefab.GetComponent<DamageDealer>();
        damageDealer.ResetDamage();
    }

    void Update()
    {
        Move();
        Fire();
        SlowDown();
    }


    //////////////////////////////////
    /////////// COLLUSIONS ///////////
    //////////////////////////////////   

    private void OnTriggerEnter2D(Collider2D other){
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        HealthPowerup healthPowerUp = other.gameObject.GetComponent<HealthPowerup>();
        LaserPowerup laserPowerUp = other.gameObject.GetComponent<LaserPowerup>();
        BossHealthPowerup bossHealthPowerUp = other.gameObject.GetComponent<BossHealthPowerup>();
        BossLaserPowerup bossLaserPowerUp = other.gameObject.GetComponent<BossLaserPowerup>();
        if (damageDealer){
            ProcessHit(damageDealer);
        }
        else if (healthPowerUp){
            if(health <= 2500)
                ProcessHpPowerUp(healthPowerUp);
        }
        else if (laserPowerUp){
            ProcessLaserPowerUp(laserPowerUp);
        }
        else if (bossHealthPowerUp){
            if(health <= 2500)
                ProcessBossHpPowerUp(bossHealthPowerUp);
        }
        else if (bossLaserPowerUp){
            ProcessBossLaserPowerUp(bossLaserPowerUp);
        }
    }


    //////////////////////////////////
    ///////////// METHODS ////////////
    //////////////////////////////////    

    // Those will be the borders of the game, and the camera.
    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    // This method is to move the player with the help of input manager.
    private void Move()
    {
        var deltaX = Input.GetAxisRaw("Horizontal") * Time.deltaTime * moveSpeed;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);

        // I'm following the same procedure for the Y axis.
        var deltaY = Input.GetAxisRaw("Vertical") * Time.deltaTime * moveSpeed;
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        // Change the player pos with the new X and Y values.
        transform.position = new Vector2(newXPos, newYPos);
        StartCoroutine("EngineFire");

    }

    // This method is to slow down the player while "ALT" is clicked.
    void SlowDown(){
        
        if(Input.GetButtonDown("Slow")){
            moveSpeed = slowMoveSpeed;
        }
        if(Input.GetButtonUp("Slow")){
            moveSpeed = constMoveSpeed;            
        }        
    }

    // If player pushes the "Fire1" button, FiringCoroutine, which will start 
    // FireContiniously Coroutine. And when the player stops pushing it,
    // the method will stop. 
    private void Fire()
    {
        if(Input.GetButtonDown("Fire1")){
            FiringCoroutine = StartCoroutine(FireContiniously());
        }
        if(Input.GetButtonUp("Fire1")){
            StopCoroutine(FiringCoroutine);
            StartCoroutine("BackFire");              
        }
    }

    // This coroutine is to shot lasers. It creates clones of the laserPrefab as 
    // GameObjects. The reason why I've used coroutine is to introduce a delay 
    // between shots. So that they won't seem like a straight line while calling 
    // multiple of them by keep pressing "space" button.
    IEnumerator FireContiniously()
    {
        while (true){
            AudioSource.PlayClipAtPoint(laserSound, transform.position, 1.3f);
            Vector2 laserPosition = new Vector2(transform.position.x, transform.position.y + 1.1f);
            GameObject laser = Instantiate(laserPrefab, laserPosition, Quaternion.identity);
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserSpeed);
            StartCoroutine("BackFire");
            yield return new WaitForSeconds(laserFiringPeriod);

        }
    }

    // This method is to process the hits on the player.
    private void ProcessHit(DamageDealer damageDealer){
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0){
            Die();
        }
    }

    // This method is to process the HP power ups on the player.
    private void ProcessHpPowerUp(HealthPowerup hpBox)
    {
        health += hpBox.GetHPPowerUp();
        hpBox.Hit();
    }

    // This method is to process the boss HP power ups on the player.
    private void ProcessBossHpPowerUp(BossHealthPowerup bossHpBox)
    {
        health += bossHpBox.GetBossHpPowerUp();
        bossHpBox.Hit();
    }

   // This method is to process the laser power ups on the player.
    private void ProcessLaserPowerUp(LaserPowerup laserBox)
    {
        DamageDealer damageDealer = laserPrefab.GetComponent<DamageDealer>();
        damageDealer.IncreaseDamage();
        laserBox.Hit();
    }

   // This method is to process the boss laser power ups on the player.
    private void ProcessBossLaserPowerUp(BossLaserPowerup bossLaserBox)
    {
        DamageDealer damageDealer = laserPrefab.GetComponent<DamageDealer>();
        damageDealer.IncreaseDamageBoss();
        bossLaserBox.Hit();
    }

    // This method is to destroy the player while playing the relevant
    // audio and SFXs.
    private void Die()
    {
        AudioSource.PlayClipAtPoint(explosionSound, transform.position);
        Destroy(gameObject);
        GameObject explosion = Instantiate(explosionVFX, transform.position, transform.rotation);
        Destroy(explosion, durationOfExplosion);
        FindObjectOfType<Level>().LoadGameOver();
    }

    // Getter method for health. 
    public float GetHealth()
    {
        return health;
    }

    // Getter method for player laser. 
    public float GetLaserDamage()
    {
        DamageDealer damageDealer = laserPrefab.GetComponent<DamageDealer>();
        float damage = damageDealer.GetDamage();
        return damage;
    }

    // This method is to show backfire while the player is shooting.
    public IEnumerator BackFire()
    {
        Vector2 backFirePos = new Vector2(transform.position.x, transform.position.y + 0.75f);
        GameObject backFire = Instantiate(backFirePrefab, backFirePos, Quaternion.identity);
        yield return new WaitForSeconds(laserFiringPeriod);
        Destroy(backFire);
    }

    // This method is to create engine fires on the back of the player.
    public IEnumerator EngineFire()
    {
        Vector2 engineFirePos01 = new Vector2(transform.position.x - 0.2f, transform.position.y - 0.7f);
        Vector2 engineFirePos02 = new Vector2(transform.position.x + 0.24f, transform.position.y - 0.7f);
        GameObject engineFire01 = Instantiate(engineFirePrefab, engineFirePos01, Quaternion.identity);
        GameObject engineFire02 = Instantiate(engineFirePrefab, engineFirePos02, Quaternion.identity);
        yield return new WaitForSeconds(0.02f);
        Destroy(engineFire01);
        Destroy(engineFire02);
    }


}
