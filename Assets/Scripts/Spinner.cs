using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is to spin the projectiles.
public class Spinner : MonoBehaviour
{
    [SerializeField] float speedOfSpin = 45f;

    void Update()
    {
        transform.Rotate(0, 0, speedOfSpin * Time.deltaTime);
    }
}
