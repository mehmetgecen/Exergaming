using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiceScript : MonoBehaviour
{
    public GameObject lid;
    public GameObject plate;


    private void Update() {
        if(GetComponent<JarLidScript>().isMoveable == false && plate.GetComponent<PlateScript>().state !=-1
        && lid.GetComponent<JarLidScript>().initPos != lid.transform.position 
        && GetComponent<JarLidScript>().initPos == transform.position)
        {
            GetComponent<JarLidScript>().isMoveable = true;
        }

        if(plate.GetComponent<PlateScript>().state == 2 && GetComponent<JarLidScript>().initPos == transform.position)
        {
            gameObject.SetActive(false);
        }
    }

    
}
