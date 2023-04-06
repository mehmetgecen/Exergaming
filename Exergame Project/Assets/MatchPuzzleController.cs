using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchPuzzleController : MonoBehaviour
{
    public LevelManager levelManager;
    public GameObject mission_10;
    public GameObject box;
    
    private void Update() {
        if( box.GetComponent<MatchPuzzle>().isMatchCompleted || Input.GetKeyDown(KeyCode.Space)){

            gameObject.SetActive(false);
        }
    }
    private void OnDisable() {
        mission_10.SetActive(true);
        levelManager.SkipMission();
    }
   
}
