using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class represents the laser power up. Which is a little box upgrading the player laser. It will drop from regular enemies.
public class LaserPowerup : MonoBehaviour
{
    // This method is to destroy the power up box once it's 
    // collided with the player.
    public void Hit()
    {
        Destroy(gameObject);
    }
}
