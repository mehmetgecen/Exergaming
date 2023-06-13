using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class MatchPuzzle : MonoBehaviour
{
    public bool isMatchCompleted;
    private static int matchCount = 0;
    public HandTracking handTracking;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(gameObject.tag) && other.GetComponent<Interactable>())
        {
            matchCount++;
            other.gameObject.SetActive(false);
            Debug.Log(matchCount);
            
            Debug.Log("First Match Completed.");
        }
        

        if (matchCount >= 2 && (!other.gameObject.CompareTag(gameObject.tag)) && other.GetComponent<Interactable>())
        {
            Debug.Log(other.gameObject.name);
            
            gameObject.SetActive(false);
            other.gameObject.SetActive(false);

            isMatchCompleted = true;

            Debug.Log("Final Match Completed.");
            
            
        }
        
        
    }
    
    
}
