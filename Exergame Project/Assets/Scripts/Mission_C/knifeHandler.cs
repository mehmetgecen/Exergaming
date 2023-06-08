using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knifeHandler : MonoBehaviour
{
    public bool isMoveable = true;
    public Vector3 offset;
    public GameObject handPivot;
    public GameObject currentMission;
    public GameObject nextMission;
    public LevelManager levelManager;

    

    
    private void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("HandObjects"))
        {            

            string handType = other.transform.parent.tag == "RightHand" ? "right" : "left";
            bool isGrabbing = EventManager.isGrabbing.Invoke(handType);

            Debug.Log(isGrabbing);
            // hand is touched and grabbing
            if (isGrabbing && isMoveable)
            {                
                transform.position = handPivot.transform.position + offset;
            }
        }
    }

    public void endScenerio(){
            nextMission.SetActive(true);
            levelManager.SkipMission();
            currentMission.SetActive(false);
    }
}
