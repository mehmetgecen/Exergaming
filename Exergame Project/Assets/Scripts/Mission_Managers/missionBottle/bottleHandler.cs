using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bottleHandler : MonoBehaviour
{
    public string[] bottleOrder;
    public GameObject[] waterColors;

    public LevelManager levelManager;
    public GameObject nextMission;

    private int counter = 0;
    private void OnTriggerEnter(Collider other) {   
        if (other.tag == "bottle" && bottleOrder[counter] == other.GetComponent<bottleStatus>().waterColor)
        {
            waterColors[counter].SetActive(true);
            counter++;
            other.GetComponent<bottleStatus>().isMoveable = false;
            other.GetComponent<bottleStatus>().isFinished = true;
            other.transform.position = other.GetComponent<bottleStatus>().initPos;            
            Debug.Log(counter);      
        }
    }

    private void Update() {
        if(counter >= 3){
            nextMission.SetActive(true);
            levelManager.SkipMission();
            this.enabled = false;
        }
    }
}
