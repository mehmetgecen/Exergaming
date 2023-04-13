using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleaning : MonoBehaviour
{
    public LevelManager levelManager;
    public int cleanState = 0;// for different dirt phases.
    public GameObject[] ghostSpongePos; // 0 - 3
    public GameObject[] dirtAreas; // 0 - 3
    public GameObject currentMission;
    public GameObject nextMission;
    public GameObject handPivot;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("HandObjects"))
        {            

            string handType = other.transform.parent.tag == "RightHand" ? "right" : "left";
            bool isGrabbing = EventManager.isGrabbing.Invoke(handType);

            Debug.Log(isGrabbing);
            // hand is touched and grabbing
            if (isGrabbing)
            {                
                transform.position = handPivot.transform.position;
            }
        }
    }

    public void nextState(){
        if(cleanState<3){
            ghostSpongePos[cleanState].SetActive(false);
            dirtAreas[cleanState].SetActive(false);
            cleanState++;
            ghostSpongePos[cleanState].SetActive(true);
        }
        else{
            nextMission.SetActive(true);
            levelManager.SkipMission();
            currentMission.SetActive(false);
        }
        
        
    }

   

    
    
}
