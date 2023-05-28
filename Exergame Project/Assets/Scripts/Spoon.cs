using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spoon : MonoBehaviour
{
    public LevelManager levelManager;
    
    [Header("Lap Control Elements")]
    public Collider[] lapColliders;
    public bool l1,l2,l3,l4;
    private int lapCount;
    private int targetLapNumber = 3;
    
    private float _currentRotation;
    private float _speed = 50f;
    
  
    private void OnTriggerEnter(Collider other)
    {
        // clockwise and counter clockwise controls must be implemented.
        // circular lap must be broken if next correct trigger didn't happen.
        
            if (other.CompareTag("LapObject1"))
            { 
                l1 = true;
                Debug.Log("L1");
            }
            
            if (other.CompareTag("LapObject2"))
            {
                l2 = true;
                Debug.Log("L2");
            }
            
            if (other.CompareTag("LapObject3"))
            {
                l3 = true;
                Debug.Log("L3");
            }
            
            if (other.CompareTag("LapObject4"))
            {
                l4 = true;
                Debug.Log("L4");
            }


            if (l1 && l2 && l3 && l4)
            {
                lapCount++;
                Debug.Log("Lap Completed "+ "Current Lap: " + lapCount);
                ResetLapElements();
                
            }

            if (lapCount >= targetLapNumber)
            {
                gameObject.SetActive(false);
                levelManager.SkipMission();
            }

    }

    private void ResetLapElements()
    {
        l1 = false;
        l2 = false;
        l3 = false;
        l4 = false;
    }
}

