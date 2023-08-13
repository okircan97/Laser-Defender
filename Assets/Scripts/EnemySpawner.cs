using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //////////////////////////////////
    ///////////// FIELDS /////////////
    //////////////////////////////////    

    [SerializeField] List<WaveConfig> waveConfigs;

    // The index of the waveConfigs.
    [SerializeField] int startingWave = 0;          
    [SerializeField] bool looping = false;         
    int spawnedWaveIndex;

    //////////////////////////////////
    ///////// START & UPDATE /////////
    //////////////////////////////////

    // The reason why I'm using teh Start method as a coroutine is to prevent 
    // the Unity engine from freezing while using endless loops inside of it.
    IEnumerator Start(){
        do{
            yield return StartCoroutine(SpawnAllWaves());
        }
        // We could also use a "while(true)" loop as well. But, in that case, 
        // it wouldn't be possible to stop or start the loop over the inspector.
        while (looping);                           
                                                   
    }


    //////////////////////////////////
    ///////////// METHODS ////////////
    //////////////////////////////////   

    // This coroutine is to spawn all the waves.
    private IEnumerator SpawnAllWaves()
    {
        for(int waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++){

            var currentWave = waveConfigs[waveIndex];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
        spawnedWaveIndex++;
    }

    // This coroutine is to spawn all the enemies inside a wave.
    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig){
        for (int enemyCount = 0; enemyCount < waveConfig.GetNumberOfEnemies(); enemyCount++){
            var newEnemy = Instantiate(
                waveConfig.GetEnemyPrefab(),
                waveConfig.GetWaypoints()[0].transform.position,
                Quaternion.identity); 
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);   
            if(spawnedWaveIndex != 0){
                newEnemy.GetComponent<Enemy>().UpgradeEnemy();
                Debug.Log("enemies upgraded");
            }
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
    }


}
