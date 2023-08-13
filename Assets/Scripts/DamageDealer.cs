using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Other classes and their objects will get their damage values from this class.
public class DamageDealer : MonoBehaviour
{
    //////////////////////////////////
    ///////////// FIELDS /////////////
    //////////////////////////////////

    [SerializeField] float damage = 100;


    //////////////////////////////////
    //////////// METHODS /////////////
    //////////////////////////////////

    public float GetDamage()
    {
        return damage;
    }

    // This method is to destroy the projectile after colliding with something.
    public void Hit()
    {
        Destroy(gameObject);
    }

    // This method is to increase the damage. It'll be called after colliding 
    // with LaserPowerups.
    public void IncreaseDamage()
    {
        if(damage <= 200)
            damage = damage + 0.25f;
    }

    // This method is to increase the damage. It'll be called after colliding 
    // with BossLaserPowerups.
    public void IncreaseDamageBoss()
    {
        if(damage <= 200)
            damage = damage + 50;
    }

    // This method is to reset the damage.
    public void ResetDamage()
    {
        damage = 100;
    }

}
