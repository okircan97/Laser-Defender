using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is  to move the enemy along current path.
public class EnemyPathing : MonoBehaviour
{
    //////////////////////////////////
    ///////////// FIELDS /////////////
    //////////////////////////////////    

    WaveConfig waveConfig;            
    List<Transform> waypoints;        
    // An index to keep track of the waypoints.
    int waypointIndex = 0;            
    [SerializeField] bool looping = true;


    //////////////////////////////////
    ///////// START & UPDATE /////////
    //////////////////////////////////

    void Start()
    {
        waypoints = waveConfig.GetWaypoints();   
        // The enemies' start position is where the first element of the waypoints is.
        transform.position = waypoints[waypointIndex].position; 
    }

    private void Update()
    {
        Move();
    }
 

    //////////////////////////////////
    ///////////// METHODS ////////////
    //////////////////////////////////   

    // Setter method for wave config.
    public void SetWaveConfig(WaveConfig waveConfig){
        this.waveConfig = waveConfig;
    } 

    // This method is to make the enemies move.
    private void Move(){
        if(waypointIndex <= waypoints.Count - 1){
            var targetPosition = waypoints[waypointIndex].position;  
            var movementThisFrame = waveConfig.GetMoveSpeed() * Time.deltaTime;  
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementThisFrame);
            if (transform.position == targetPosition){                                                        
                waypointIndex++;                                     
            }
        }
        else{
            if(!looping)
                Destroy(gameObject);
            else
                waypointIndex = 0;
        }
    }
}


