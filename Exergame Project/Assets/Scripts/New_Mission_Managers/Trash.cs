using System;
using UnityEngine;
using DG.Tweening;

public class Trash : MonoBehaviour
{
    #region variables for mission control
    public HandTracking handTracking;
    public LevelManager levelManager;
    public Mission_5_UI_Control recipeController;
    private bool isMissionDone;
    public Transform[] formerPositions;

    public GameObject currentMission;
    public GameObject prevMission;

    #endregion

    private int place_counter = 0;
    [SerializeField] private int capacity = 5;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Interactable>() && other.gameObject.name == "holdable") // Recipe Control will be added;
        {
            handTracking.ResetHand(other.gameObject.GetComponent<Interactable>().handType);
            
            other.gameObject.name = "sss";
            other.GetComponent<Interactable>().enabled = false ;
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            other.gameObject.GetComponent<Collider>().enabled = false;
            other.gameObject.transform.DOMove(transform.GetChild(place_counter).position, 0.5f);
            other.gameObject.transform.DORotate(transform.GetChild(place_counter).eulerAngles, 0.5f);

            // Play Success sound.

            place_counter++;
        }

        if (place_counter >= capacity && !isMissionDone)
        {
            isMissionDone = true;        
            levelManager.SkipMission();
            currentMission.SetActive(false);
            prevMission.SetActive(false);
        }
    }
}