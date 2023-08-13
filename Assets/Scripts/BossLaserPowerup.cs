using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class represents the boss laser power up. Which is a little box upgrading the player laser. It will drop from the bosses.
public class BossLaserPowerup : MonoBehaviour
{
    // This method is to destroy the power up box after colliding with the player.
    public void Hit()
    {
        Destroy(gameObject);
    }
}
