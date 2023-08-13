using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class includes waves configuration informations. 

// ScriptableObjects are true standalone objects. They are serialized on their own and the MonoBehaviour will just hold the reference  
// to the instance, even when serialized. Scriptable objects can exist in your Assets folder, and unlike a MonoBehaviour object, they do not have a 
// Transform or irrelevant objects they would need if they existed in the scene. They are containers in which you can store data. 
// Türkçesi şu ki: Game hierarchy'nin içine sürüklemeden, assetler içinde yarattığımız "Enemy Wave Configuration"larını, "Enemy Spawner" içinde 
// kullanabileceğiz.

[CreateAssetMenu(menuName = "Enemy Wave Configuration")]
public class WaveConfig : ScriptableObject
{
    //////////////////////////////////
    ///////////// FIELDS /////////////
    //////////////////////////////////

    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float timeBetweenSpawns = 0.5f;
    [SerializeField] float spawnRandomFactor = 0.3f;
    [SerializeField] int numberOfEnemies = 5;
    [SerializeField] float moveSpeed = 2f;


    //////////////////////////////////
    //////////// METHODS /////////////
    //////////////////////////////////

    // Getter method for enemyPrefab.
    public GameObject GetEnemyPrefab() {
        return enemyPrefab;
    }

    // Getter method for waypoints' transform infos. 
    public List<Transform> GetWaypoints(){
        var waveWaypoints = new List<Transform>();
        foreach (Transform child in pathPrefab.transform){
            waveWaypoints.Add(child);
        }
        return waveWaypoints;
    }

    // Getter method for timeBetweenSpawns.
    public float GetTimeBetweenSpawns(){
        return timeBetweenSpawns;
    }

    // Getter method for spawnRandomFactor.
    public float GetSpawnRandomFactor(){
        return spawnRandomFactor;
    }

    // Getter method for numberOfEnemies.
    public int GetNumberOfEnemies(){
        return numberOfEnemies;
    }

    // Getter method for moveSpeed.
    public float GetMoveSpeed(){
        return moveSpeed;
    }


}
