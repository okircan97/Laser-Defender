using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class represents the boss hp power up. Which is a little box upgrading the player health. It will drop from the bosses.
public class BossHealthPowerup : MonoBehaviour
{
    //////////////////////////////////
    ///////////// FIELDS /////////////
    //////////////////////////////////

    [SerializeField] float hpPowerUp = 500;


    //////////////////////////////////
    //////////// METHODS /////////////
    //////////////////////////////////
    
    public float GetBossHpPowerUp()
    {
        return hpPowerUp;
    }

    // This method is to destroy the power up box after colliding with the player.
    public void Hit()
    {
        Destroy(gameObject);
    }
}
