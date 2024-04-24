using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class enemyTriggerScript : MonoBehaviour
{   
    public LevelLoader loadScene;
    public AudioManager audios;
    private float chanceEncounter;
    public MoveScript movescript;
    public void EnemyTrigger(){
        movescript.canMove = false;
        chanceEncounter = Random.Range(1,100);
        if(chanceEncounter >= 80){
            Debug.Log("ENCOUNTER START!");
            movescript.canMove = true;
            loadScene.LoadXLevel(2);
            
            //load transition and scene
            
           
        }
        else if(chanceEncounter < 80){
            movescript.canMove = true;
            Debug.Log("NO ENCOUNTER");
            
            
        }
        movescript.canMove = true;
        
    }
}
