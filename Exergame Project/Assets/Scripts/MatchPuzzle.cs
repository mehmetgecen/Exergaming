using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class MatchPuzzle : MonoBehaviour
{
    public LevelManager levelManager;
    public GameObject[] cubes;
    //public GameObject dirtArea;
    //public GameObject sponge;
    public bool isMatchCompleted;
    
    private static int matchCount = 0;

    
    
    // Start is called before the first frame update

    private void Start()
    {
        cubes[0].GetComponent<MeshRenderer>().material.color = Color.red;
        cubes[1].GetComponent<MeshRenderer>().material.color = Color.green;
        cubes[2].GetComponent<MeshRenderer>().material.color = Color.green;
        cubes[3].GetComponent<MeshRenderer>().material.color = Color.red;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<MeshRenderer>().material.color == gameObject.GetComponent<MeshRenderer>().material.color && other.gameObject.name =="holdable")
        {
            matchCount++;
            other.gameObject.SetActive(false);
            Debug.Log(matchCount);
            
            Debug.Log("First Match Completed.");
            
        }
        
        if (matchCount >= 2 && (other.gameObject.GetComponent<MeshRenderer>().material.color != gameObject.GetComponent<MeshRenderer>().material.color) && other.gameObject.name == "holdable")
        {
            Debug.Log(other.gameObject.name);
            
            //dirtArea.SetActive(true);
            //sponge.SetActive(true);
            
            gameObject.SetActive(false);
            other.gameObject.SetActive(false);

            isMatchCompleted = true;

            Debug.Log("Final Match Completed.");
            
            
        }
    }
    
    
}
