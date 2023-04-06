using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JarLidPosScript : MonoBehaviour
{
    public GameObject plate;
    public GameObject lid;
    public GameObject spice;

    public LevelManager levelManager;
    public GameObject currentMission;
    public GameObject nextMission;

    private int _triggerCounter = 0;
    private void OnTriggerEnter(Collider other) {
        if(other.name == "JarLid")
        {
            other.GetComponent<JarLidScript>().isMoveable = false;
            other.transform.position = transform.position;
            spice.GetComponent<JarLidScript>().isMoveable = true;

            Debug.Log("girdi");
            _triggerCounter++;
        }
    }

    private void Update() {
        if(plate.GetComponent<PlateScript>().state == 2 && lid.GetComponent<JarLidScript>().isMoveable == false)
        {
            transform.position = lid.GetComponent<JarLidScript>().initPos;
            lid.GetComponent<JarLidScript>().isMoveable = true;
        }
        if(_triggerCounter >= 2 || Input.GetKeyDown(KeyCode.Space)){
            nextMission.SetActive(true);
            levelManager.SkipMission();
            currentMission.SetActive(false);
        }
    }
}
