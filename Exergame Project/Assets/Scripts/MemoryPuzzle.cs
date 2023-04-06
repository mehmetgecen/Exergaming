using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryPuzzle : MonoBehaviour
{
    public GameObject[] puzzleItems;
    public LevelManager levelManager;

    public GameObject spoon;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("HandObjects") && gameObject.name == "trueCube")
        {
            for (int i = 0; i < puzzleItems.Length; i++)
            {
                puzzleItems[i].SetActive(false);
            }
            
            Debug.Log("Success.");
            
            levelManager.SkipMission();
            spoon.SetActive(true);
            
            
            // Camera angle and position can be changed.
            // Start shake scenario.
        }
        
        

    }
    
}
