using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class represents the health power up. Which is a little box upgrading the player health. It will drop from regular enemies.
public class HealthPowerup : MonoBehaviour
{
    //////////////////////////////////
    ///////////// FIELDS /////////////
    //////////////////////////////////
    float hpPowerUp;


    //////////////////////////////////
    //////////// METHODS /////////////
    //////////////////////////////////

    // Getter for the HP power up.
    public float GetHPPowerUp()
    {
        hpPowerUp = Random.Range(10, 20);
        return hpPowerUp;
    }

    // This method is to destroy the power up box once it's 
    // collided with the player.
    public void Hit()
    {
        Destroy(gameObject);
    }
}
